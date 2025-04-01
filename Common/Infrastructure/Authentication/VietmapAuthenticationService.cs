using Application.Core.Authentication;
using Contract.Authentication;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace Infrastructure.Authentication;

public sealed class VietmapAuthenticationService(IConfiguration configuration) :
    IVietmapAuthenticationService
{
    private readonly RestClient _restClient = new(configuration["VietmapSooUrl"]!);

    public async Task<Result<Email>> CheckProfile(string clientId, string accessToken)
    {
        RestRequest request = new RestRequest { Method = Method.Get };
        request.AddHeader("Authorization", $"Bearer {accessToken}");
        request.AddParameter("clientId", clientId);

        var response = await _restClient.ExecuteAsync<VietmapSsoResponse>(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Result.Failure<Email>(new Error(response.ErrorMessage ?? "", "Unauthorized"));
        }

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return Result.Failure<Email>(new Error(response.ErrorMessage ?? "",
                "Can not get profile from Vietmap SSO"));
        }

        return Email.Create(response.Data?.Data?.Profile?.Email ?? "");
    }
}