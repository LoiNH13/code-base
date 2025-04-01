namespace OdooPayment.Background.Contracts
{
    internal class KafkaCortexSmsBanking
    {
        public string? ClientId { get; set; }

        public string? ClientRequestId { get; set; }

        public string? RequestType { get; set; }

        public required string RequestCode { get; set; }

        public required string BankName { get; set; }

        public Guid TrackingRequestId { get; set; }

        public List<TransactionHistoryModel> Transactions { get; set; } = [];
    }

    public class TransactionHistoryModel
    {
        public Guid TransactionId { get; set; }

        public required string TransactionStatus { get; set; }

        public string? TransactionChannel { get; set; }

        public string? TransactionCode { get; set; }

        public required string AccountNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string? DebitOrCredit { get; set; }

        public string? VaPrefixCd { get; set; }

        public string? VaNbr { get; set; }

        public decimal Amount { get; set; }

        public required string TransactionContent { get; set; }

        public List<TransactionHistoryDynamicValueModel> TransactionHistoryDynamicValues { get; set; } = [];
    }

    public class TransactionHistoryDynamicValueModel
    {
        public string? AttributeName { get; set; }

        public string? Value { get; set; }
    }
}
