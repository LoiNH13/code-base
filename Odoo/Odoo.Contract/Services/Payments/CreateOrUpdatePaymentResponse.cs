namespace Odoo.Contract.Services.Payments
{
    public class CreateOrUpdatePaymentResponse
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
