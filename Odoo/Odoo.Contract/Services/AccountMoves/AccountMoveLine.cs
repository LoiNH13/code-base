namespace Odoo.Contract.Services.AccountMoves
{
    public struct AccountMoveLine
    {
        public required int Partner { get; set; }

        public required string Account { get; set; }

        public required string Description { get; set; }

        public required double Debit { get; set; }

        public required double Credit { get; set; }
    }
}
