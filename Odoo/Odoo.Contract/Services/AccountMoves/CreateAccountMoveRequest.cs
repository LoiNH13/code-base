using Odoo.Contract.Services.Abstracts;
using System.Text.Json;

namespace Odoo.Contract.Services.AccountMoves
{
    public class CreateAccountMoveRequest : IOdooRequest
    {
        public string _path => "api/v1/call_kw_model/account.move/create_account_move_from_api";

        public required string AccountAnalyticCode { get; set; }

        public required string JournalCode { get; set; }

        public required string DocDate { get; set; }

        public required string Date { get; set; }

        public required string Ref { get; set; }

        public required string SourceDocument { get; set; }

        public required string Source { get; set; }

        public required AccountMoveLine[] MoveLine { get; set; }

        public string GetPath() =>
            _path
            + $"?account_analytic_code={AccountAnalyticCode}"
            + $"&journal_code={JournalCode}"
            + $"&doc_date={DocDate}"
            + $"&date={Date}"
            + $"&ref={Ref}"
            + $"&source_document={SourceDocument}"
            + $"&source={Source}"
            + $"&move_line={JsonSerializer.Serialize(MoveLine)}";
    }
}
