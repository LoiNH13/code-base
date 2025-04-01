namespace MOT.Contract.Core.VietmapLive;

public class VietmapLiveResponseTemplate<T> where T : class
{
    public T? Data { get; set; }

    public ErrorResponse? Error { get; set; }
}

public class ErrorResponse
{
    public string ResponseCode { get; set; } = default!;

    public string Description { get; set; } = default!;
}