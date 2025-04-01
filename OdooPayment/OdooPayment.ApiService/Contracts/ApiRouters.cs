namespace OdooPayment.ApiService.Contracts
{
    public static class ApiRouters
    {
        public static class Internals
        {
            public const string CreateOdooPayment = "internals/payment/odoo";

            public const string ConfirmPaymentSms = "internals/payment/sms/confirm";

            public const string Customers = "internals/customers";
        }
    }
}
