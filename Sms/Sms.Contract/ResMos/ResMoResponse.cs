namespace Sms.Contract.ResMos;

/// <summary>
///     Represents a response model for a mobile service subscription.
/// </summary>
/// <param name="id">The unique identifier for the response.</param>
/// <param name="servicePhone">The phone number associated with the service.</param>
/// <param name="pricePerMo">The price per month for the service.</param>
/// <param name="freeMtPerMo">The amount of free minutes per month included in the service.</param>
public class ResMoResponse(Guid id, string servicePhone, double pricePerMo, double freeMtPerMo)
{
    public Guid Id { get; set; } = id;

    public string ServicePhone { get; set; } = servicePhone;

    public double PricePerMo { get; set; } = pricePerMo;

    public double FreeMtPerMo { get; set; } = freeMtPerMo;
}