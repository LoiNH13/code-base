using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Domain.Core.Utility;
using Microsoft.EntityFrameworkCore;
using Odoo.Application.Core.Odoo;
using Odoo.Contract.Services.Payments;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using OdooPayment.Application.Core.Odoo;
using Payment.Persistence.Models;

namespace OdooPayment.Infrastructure.Odoo
{
    internal sealed class OdooIntegrationService : IOdooIntegrationSerivce
    {
        readonly IOdooServices _odooServices;
        readonly IOdooOrderRepository _odooOrderRepository;
        readonly IOdooAccountJournalRepository _odooAccountJournalRepository;
        readonly IOdooAccountMoveRepository _odooAccountMoveRepository;

        public OdooIntegrationService(IOdooServices odooServices,
                                      IOdooOrderRepository odooOrderRepository,
                                      IOdooAccountJournalRepository odooAccountJournalRepository,
                                      IOdooAccountMoveRepository odooAccountMoveRepository)
        {
            _odooServices = odooServices;
            _odooOrderRepository = odooOrderRepository;
            _odooAccountJournalRepository = odooAccountJournalRepository;
            _odooAccountMoveRepository = odooAccountMoveRepository;
        }

        public async Task<Result> CreateOdooPayment(PaymentSm paymentSms)
        {
            if (string.IsNullOrWhiteSpace(paymentSms.OdooSonumber) || paymentSms.InboundPaymentIsConfirm.HasValue)
                return Result.Failure(new Error("CreateOdooPayment.Invalid", "Invalid payment details."));

            string bankNumber = paymentSms.SmsbankNumber;
            if (paymentSms.SmsbankName == "BIDV")
            {
                var splitBankNbr = bankNumber.Split("xxx");
                if (splitBankNbr.Count() >= 2)
                {
                    bankNumber = $"{paymentSms.SmsbankName} {splitBankNbr.FirstOrDefault()}";
                }
            }

            //params cần để tạo payment odoo 
            Maybe<AccountJournal> mbAccountJournal = await _odooAccountJournalRepository.GetByContainstName(bankNumber);

            string note = $"{paymentSms.OdooCustomerName} - {paymentSms.Smscontent}".ToUpper();
            string accountAnalyticCode = "AA01";
            string? invoice = default!;

            //xử lý các params cần thiết để tạo payment odoo
            string[] listSo = paymentSms.OdooSonumber.Split(",");
            if (listSo.Any())
            {
                Maybe<SaleOrder> mbSaleOrder = await _odooOrderRepository.Queryable()
                    .Include(x => x.Partner)
                    .Include(x => x.AnalyticAccount)
                    .Include(x => x.User).ThenInclude(x => x!.Partner)
                    .FirstOrDefaultAsync(x => x.Name == listSo[0] || listSo.Contains(x.Name)) ?? default!;
                if (mbSaleOrder.HasValue)
                {
                    paymentSms.SaleId = mbSaleOrder.Value.UserId ?? paymentSms.SaleId;
                    paymentSms.SaleName = mbSaleOrder.Value.User?.Partner?.Name ?? paymentSms.SaleName;
                    paymentSms.OdooCustomerCode = mbSaleOrder.Value.Partner?.Ref ?? paymentSms.OdooCustomerCode;
                    paymentSms.OdooCustomerName = mbSaleOrder.Value.Partner?.Name ?? paymentSms.OdooCustomerName;
                    accountAnalyticCode = mbSaleOrder.Value.AnalyticAccount?.Code ?? accountAnalyticCode;
                    note = $"{paymentSms.SaleName} - {paymentSms.OdooCustomerName} - {mbSaleOrder.Value.Name} - {paymentSms.Smscontent}".ToUpper(); //ko có invoice

                    Maybe<AccountMove> mbAccountMove = await _odooAccountMoveRepository.Queryable().FirstOrDefaultAsync(x => x.InvoiceOrigin == mbSaleOrder.Value.Name) ?? default!;
                    if (mbAccountMove.HasValue)
                    {
                        invoice = mbAccountMove.Value.Name;
                        note = $"{paymentSms.SaleName} - {paymentSms.OdooCustomerName} - {mbAccountMove.Value.Name} - {paymentSms.Smscontent}".ToUpper();

                        if (paymentSms.ObjectType == "Billing")
                            note = $"{paymentSms.SaleName} - {paymentSms.OdooCustomerName} - {mbAccountMove.Value.Ref} - {mbAccountMove.Value.Name} - {paymentSms.Smscontent}".ToUpper();
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(paymentSms.OdooCustomerCode) || (paymentSms.SaleId ?? 0) <= 0)
                return Result.Failure(new Error("CreateOdooPayment.Invalid", "Invalid payment details."));

            //trước khi tạo payment odoo cần
            paymentSms.InboundPaymentModified = DateTime.Now.ToString() + "\nTạo phiếu thu sang Odoo tự động bị lỗi";
            paymentSms.InboundPaymentIsConfirm = 0;
            Result<CreateOrUpdatePaymentResponse> rsCreateOdooPayment = await CreateOdooPayment(paymentSms,
                                                                                                accountAnalyticCode,
                                                                                                note,
                                                                                                invoice,
                                                                                                mbAccountJournal.Value?.Code);
            if (rsCreateOdooPayment.IsFailure)
            {
                return Result.Failure(rsCreateOdooPayment.Error);
            }

            UpdatePaymentWhenSucessful(paymentSms, rsCreateOdooPayment.Value);

            return rsCreateOdooPayment;
        }

        private static void UpdatePaymentWhenSucessful(PaymentSm paymentSms, CreateOrUpdatePaymentResponse response)
        {
            paymentSms.InboundPaymentModified = DateTime.Now.ToString() + "\nĐã tạo thành công phiếu thu sang Odoo";
            paymentSms.InboundPaymentNumber = response.Id.ToString();
            paymentSms.InboundPaymentId = response.Id;
            paymentSms.InboundPaymentIsConfirm = 0;
        }

        private async Task<Result<CreateOrUpdatePaymentResponse>> CreateOdooPayment(PaymentSm paymentSms,
                                                                                    string accountAnalyticCode,
                                                                                    string note,
                                                                                    string? invoiceName,
                                                                                    string? journalCode)
        {
            Ensure.NotEmpty(paymentSms.OdooSonumber ?? string.Empty, "CreateOdooPayment.OdooSoNumber is required", nameof(paymentSms.OdooSonumber));
            Ensure.NotEmpty(paymentSms.OdooCustomerCode ?? string.Empty, "CreateOdooPayment.OdooCustomerCode is required", nameof(paymentSms.OdooCustomerCode));
            Ensure.GreaterThan(paymentSms.SaleId ?? 0, 0, "CreateOdooPayment.SaleId is required", nameof(paymentSms.SaleId));

            return await _odooServices.CreateOrUpdatePayment(new CreateOrUpdatePaymentRequest
            {
                PaymentType = EPaymentType.Inbound,
                PartnerType = EPartnerType.Customer,
                PaymentDate = paymentSms.Smsdate,
                AccountAnalyticCode = accountAnalyticCode,
                Memo = note,
                Amount = paymentSms.Smsamount,
                Reference = invoiceName ?? paymentSms.OdooSonumber!,
                SourceDocument = paymentSms.OdooSonumber!,
                CompanyId = 1,
                JournalCode = journalCode,
                SaleUser = paymentSms.SaleId ?? 0,
                InterRef = paymentSms.Id,
                PartnerCode = paymentSms.OdooCustomerCode!,
                //Source = "platform"
            });

        }
    }
}
