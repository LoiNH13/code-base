using Domain.Core.Primitives.Maybe;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOT.Application.Core.Abstractions.VietmapLive;
using MOT.Contract.Core.VietmapLive;
using MOT.Infrastructure.VietmapLive.Settings;
using RestSharp;

namespace MOT.Infrastructure.VietmapLive;

public sealed class VietmapLiveService : IVietmapLiveService
{
    private readonly VietmapLiveSetting _vietmapLiveSetting;
    private readonly RestClient _restClient;
    private readonly ILogger _logger;

    public VietmapLiveService(IOptions<VietmapLiveSetting> options, ILogger<VietmapLiveService> logger)
    {
        _vietmapLiveSetting = options.Value;
        _restClient = new RestClient(_vietmapLiveSetting.Url);
        _logger = logger;
    }

    public async Task<Maybe<VerifyResponse>> PartnerVerify(VerifyRequest request)
    {
        var restRequest = new RestRequest(_vietmapLiveSetting.CheckSmsPath, Method.Post);

        restRequest.AddBody(request);

        var response = await _restClient.ExecuteAsync<VietmapLiveResponseTemplate<VerifyResponse>>(restRequest);

        if (response.IsSuccessful)
        {
            if (response.Data?.Data == null)
            {
                return new VerifyResponse
                {
                    ResponseCode = "01",
                    Description = response.Data?.Error?.Description ?? "Unknown Error!!",
                    Metadata = response.Content
                };
            }
            else
                response.Data.Data.Metadata = response.Content;

            return response.Data?.Data!;
        }
        else
        {
            _logger.LogError("Error while calling VietmapLive API: {0}, Status Code: {1}", response.ErrorMessage, response.StatusCode);
        }

        return new VerifyResponse
        {
            ResponseCode = "01",
            Description = response.Data?.Error?.Description ?? "Unknown Error!",
            Metadata = response.Content
        };
    }
}