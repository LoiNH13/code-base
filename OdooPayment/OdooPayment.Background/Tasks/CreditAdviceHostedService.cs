using Confluent.Kafka;
using Domain.Core.Primitives.Result;
using Integration.Kafka.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Odoo.Domain.Repositories;
using Odoo.Domain.ValueObjects;
using OdooPayment.Application.Core.Odoo;
using OdooPayment.Background.Contracts;
using OdooPayment.Background.Contracts.Settings;
using Payment.Persistence.Models;
using System.Text.RegularExpressions;

namespace OdooPayment.Background.Tasks
{
    public class CreditAdviceHostedService : BackgroundService
    {
        private readonly BankAcountSetting _bankAcountSetting;
        private readonly ILogger _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceProvider _serviceProvider;
        private static readonly Regex _validRegex = new Regex(OrderCode.ValidPattern);

        public CreditAdviceHostedService(ILogger<CreditAdviceHostedService> logger,
                                          IOptions<KafkaSetting> options,
                                          IOptions<BankAcountSetting> bankAccountOption,
                                          IServiceProvider serviceProvider)
        {
            _bankAcountSetting = bankAccountOption.Value;
            _logger = logger;
            _consumer = new ConsumerBuilder<string, string>(options.Value.BuildDefaultConfig())
                .SetErrorHandler((_, e) => _logger.LogError("Kafka error: {@Reason}", e.Reason))
                .Build();
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(KafkaTopics.OdooWorker.KafkaCortexSmsBanking);
            _ = Task.Run(async () =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var result = _consumer.Consume(stoppingToken);

                            if (result != null)
                            {
                                await ProcessMessageAsync(result.Message.Value);
                            }
                        }
                        catch (OperationCanceledException ex)
                        {
                            _logger.LogInformation(ex, "Operation canceled");
                            //cancel token
                            break;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error consuming Kafka message");
                            break;
                        }
                    }
                });
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(string message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var odooOrderRepository = scope.ServiceProvider.GetRequiredService<IOdooOrderRepository>();
                var odooCustomerRepository = scope.ServiceProvider.GetRequiredService<IOdooCustomerRepository>();
                var odooIntegrationSerivce = scope.ServiceProvider.GetRequiredService<IOdooIntegrationSerivce>();
                var paymentDbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();

                //deserialize message into KafkaCortexSmsBanking
                var bankingSms = JsonConvert.DeserializeObject<KafkaCortexSmsBanking>(message);
                if (bankingSms == null)
                {
                    //make log with message content
                    _logger.LogWarning("Cannot deserialize message: {Message}", message);
                    return;
                }
                else if (bankingSms.BankName == "ACB" && bankingSms.RequestCode == "TRANSACTION_UPDATE")//ghi báo có
                {
                    var checkRegex = _validRegex.Matches(bankingSms.Transactions[0].TransactionContent);
                    PaymentSm paymentSms = MapToPaymentSms(bankingSms, string.Join(",", checkRegex));

                    //dựa vào so để ktra xem ai là người chuyển. (ưu tiên 1)
                    if (checkRegex.Count > 0)
                    {
                        Result<OrderCode> rsOrderCode = OrderCode.Create(checkRegex.FirstOrDefault(checkRegex =>
                                                                                                    OrderCode.Create(checkRegex.Value).IsSuccess)?
                                                                                                   .ToString() ?? string.Empty);
                        if (rsOrderCode.IsSuccess)
                        {
                            var mbSaleOrder = await odooOrderRepository.GetReportSaleOrderViewByOrderName(rsOrderCode.Value);
                            if (mbSaleOrder.HasValue)
                            {
                                paymentSms.SaleId = mbSaleOrder.Value.SalespersonId;
                                paymentSms.SaleName = mbSaleOrder.Value.Salesperson;
                                paymentSms.OdooCustomerCode = mbSaleOrder.Value.InternalRef;
                                paymentSms.OdooCustomerName = mbSaleOrder.Value.CustomerName;
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(bankingSms.Transactions[0].VaNbr))//dựa vào số tài khoản để ktra xem ai là người chuyển. (nếu có va thì phải xác định đúng customer)
                    {
                        var vaNumbers = bankingSms.Transactions[0].VaNbr?.Split(bankingSms.Transactions[0].VaPrefixCd) ?? [];
                        if (vaNumbers.Length >= 2)
                        {
                            paymentSms.OdooCustomerCode = bankingSms.Transactions[0].VaNbr!.Split(bankingSms.Transactions[0].VaPrefixCd)[1];
                            if (paymentSms.OdooCustomerCode.EndsWith('N'))
                            {
                                paymentSms.OdooCustomerCode = paymentSms.OdooCustomerCode.Split('N')[0];

                                var mbCustomer = await odooCustomerRepository.GetCustomerByOdooRef(paymentSms.OdooCustomerCode);
                                if (mbCustomer.HasValue)
                                {
                                    paymentSms.OdooCustomerName = mbCustomer.Value.Name;
                                }
                            }
                        }
                    }
                    await odooIntegrationSerivce.CreateOdooPayment(paymentSms);

                    paymentDbContext.Add(paymentSms);
                    await paymentDbContext.SaveChangesAsync();
                }
            }
        }

        private PaymentSm MapToPaymentSms(KafkaCortexSmsBanking bankingSms, string? listSO) => new PaymentSm
        {
            Smsphone = "0",
            SmsbankName = bankingSms.BankName,
            Method = "Chuyển khoản",
            Smsdate = bankingSms.Transactions[0].TransactionDate,
            Smsamount = bankingSms.Transactions[0].Amount,
            SmsbankNumber = bankingSms.Transactions[0].AccountNumber,
            Smscontent = bankingSms.Transactions[0].TransactionContent,
            Vaaccount = bankingSms.Transactions[0].VaNbr,
            BranchId = _bankAcountSetting.HN.Split(',').Contains(bankingSms.Transactions[0].AccountNumber) ? 2 : 1,
            OdooSonumber = listSO
        };
    }
}
