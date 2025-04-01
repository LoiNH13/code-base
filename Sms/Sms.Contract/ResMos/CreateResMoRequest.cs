namespace Sms.Contract.ResMos;

public class CreateResMoRequest
{
    public required string ServicePhone { get; set; }
    public double PricePerMo { get; set; }
    public double FreeMtPerMo { get; set; }
}