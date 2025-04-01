using Odoo.Contract.Services.Abstracts;

namespace Odoo.Contract.Services.AccountMoves
{
    public class CancelAccountMoveRequest : IOdooRequest
    {
        public string _path => "/api/v1/call_kw_model/account.move/cancel_account_move_from_api";

        public required int Id { get; set; }

        public string GetPath() => _path + $"?id={Id}";
    }
}
