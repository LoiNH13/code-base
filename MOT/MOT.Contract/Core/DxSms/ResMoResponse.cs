namespace MOT.Contract.Core.DxSms;

public class ResMoResponse
{
    public Guid Id { get; set; }

    public string ServicePhone { get; set; } = null!;

    public double PricePerMo { get; set; }

    public double FreeMtPerMo { get; set; }
}