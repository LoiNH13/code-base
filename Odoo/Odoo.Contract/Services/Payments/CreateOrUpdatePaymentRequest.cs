using Odoo.Contract.Services.Abstracts;

namespace Odoo.Contract.Services.Payments
{
    public class CreateOrUpdatePaymentRequest : IOdooRequest
    {
        public string _path => "api/v1/call_kw_model/account.payment/create_payment_from_api";

        public required int SaleUser { get; set; }

        public required string PartnerCode { get; set; }

        public required string AccountAnalyticCode { get; set; }

        public required string Memo { get; set; }

        public string? Reference { get; set; }

        public required string SourceDocument { get; set; }

        public required decimal Amount { get; set; }

        public required DateTime PaymentDate { get; set; }

        public string? JournalCode { get; set; }

        public required EPaymentType PaymentType { get; set; }

        public required EPartnerType PartnerType { get; set; }

        public string? Source { get; set; }

        public required int CompanyId { get; set; }

        public bool CashbackPayableCode { get; set; }

        public string? Name { get; set; }

        public required int InterRef { get; set; }

        public bool? AutoConfirm { get; set; }

        public string GetCashbackPayableCode() => CashbackPayableCode ? "3388" : "";

        public string GetPath() =>
            _path
            + $"?sale_user={SaleUser}"
            + $"&partner_code={PartnerCode}"
            + $"&account_analytic_code={AccountAnalyticCode}"
            + $"&memo={Memo}"
            + $"&reference={Reference ?? string.Empty}"
            + $"&source_document={SourceDocument}"
            + $"&amount={Amount}"
            + $"&payment_date={PaymentDate:yyyy-MM-dd}"
            + $"&journal_code={JournalCode ?? string.Empty}"
            + $"&payment_type={PaymentType.ToString().ToLower()}"
            + $"&partner_type={PartnerType.ToString().ToLower()}"
            + $"&source={Source}"
            + $"&company_id={CompanyId}"
            + $"&cashback_payable_code={GetCashbackPayableCode()}"
            + $"&inter_ref={InterRef}"
            + (AutoConfirm != null ? $"&auto_confirm={AutoConfirm}" : "")
            + (Name != null ? $"&name={Name}" : "");
    }
}
