using Odoo.Contract.Services.Abstracts;

namespace Odoo.Contract.Services.SaleOrders
{
    public class GetSaleOrderRequest : IOdooRequest
    {
        public string _path => "/api/v1/call_kw_model/sale.order/get_invoice_from_refid";

        public required string RefId { get; set; }

        public string GetPath() => _path + $"?refid={RefId}";
    }
}
