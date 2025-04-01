namespace MOT.Contract.Core.DxSms;

public class GetResMosRequest(int pageNumber, int pageSize, string? servicePhone)
{
    private const string Path = "api/resmos";

    public int PageNumber { get; } = pageNumber;

    public int PageSize { get; } = pageSize;

    public string ServicePhone { get; } = servicePhone ?? string.Empty;

    public string GetPath() => $"{Path}?pageNumber={PageNumber}&pageSize={PageSize}&servicePhone={ServicePhone}";
}