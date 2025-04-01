namespace MOT.Contract.Core.VietmapLive;

public sealed class VerifyRequest
{
    public string TelPhone { get; }

    public string Phone { get; }

    public string Content { get; }

    public VerifyRequest(string telPhone, string phone, string content)
    {
        TelPhone = telPhone;
        Phone = FormatPhoneNumber(phone);
        Content = content;
    }

    private static string FormatPhoneNumber(string phoneNumber)
    {
        if (phoneNumber.StartsWith("84"))
        {
            return $"+{phoneNumber}";
        }
        else if (phoneNumber.StartsWith('0'))
        {
            return $"+84{phoneNumber.Substring(1)}";
        }
        return phoneNumber;
    }
}