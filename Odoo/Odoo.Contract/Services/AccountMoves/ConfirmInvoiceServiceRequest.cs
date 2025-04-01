using Odoo.Contract.Services.Abstracts;

namespace Odoo.Contract.Services.AccountMoves
{
    public class ConfirmInvoiceServiceRequest : IOdooRequest
    {
        public string _path => "api/v1/call_kw_model/account.move/update_confirm_method_from_api";

        public required string Origin { get; set; }

        public required string CompanyAnalyticAccount { get; set; }

        public required string ConfirmMethod { get; set; }

        public string GetPath() => _path +
            $"?origin={Origin}" +
            $"&company_analytic_account={CompanyAnalyticAccount}" +
            $"&confirm_method={ConfirmMethod}";
    }
}
