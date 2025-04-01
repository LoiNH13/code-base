namespace Odoo.ApiService.Contracts
{
    public static class ApiRouters
    {
        public static class SaleOrders
        {
            public const string GetByRefId = "sale-orders/{refId}";
        }

        public static class Invoices
        {
            public const string Confirm = "invoices/confirm";
        }
    }
}
