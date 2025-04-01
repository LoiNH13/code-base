using Newtonsoft.Json;

namespace MOT.Contract.Core.VietmapLive;

public sealed class VerifyResponse
{
    public string Type { get; set; } = default!;

    public string ResponseCode { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string? Metadata { get; set; }

    public string MoResponse()
    {
        // response code must two digits if two digits then return else get frist two digits from response code
        if (ResponseCode.Length == 2)
        {
            return ResponseCode + "|" + Description;
        }
        else
        {
            return ResponseCode.Substring(0, 2) + "|" + Description;
        }
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}