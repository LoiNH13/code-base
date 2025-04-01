using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Odoo.Application.Core.Odoo;
using Odoo.Contract.Services.Abstracts;
using Odoo.Contract.Services.AccountMoves;
using Odoo.Contract.Services.Payments;
using Odoo.Contract.Services.SaleOrders;
using Odoo.Infrastructure.Odoo.Settings;
using RestSharp;
using System.Net;

namespace Odoo.Infrastructure.Odoo
{
    internal sealed class OdooServices : IOdooServices
    {
        private readonly RestClient _restClient;
        private readonly ILogger _logger;

        public OdooServices(IOptions<OdooServicesSetting> options, ILogger<OdooServices> logger)
        {
            var odooServicesSetting = options.Value;
            _restClient = new RestClient(odooServicesSetting.BaseUrl);
            _restClient.AddDefaultHeader("access-token", odooServicesSetting.AccessToken);
            _logger = logger;
        }

        public async Task<Result<AccountMoveResponse>> CancelAccountMove(int id) =>
            await CallAsync<AccountMoveResponse>(new CancelAccountMoveRequest { Id = id }).Match(
                success => Result.Success(success.Data[0]),
                Result.Failure<AccountMoveResponse>);

        public async Task<Result<ConfirmInvoiceResponse>> ConfirmInvoice(ConfirmInvoiceServiceRequest request) =>
            await CallAsync<ConfirmInvoiceResponse>(request).Match(
                success => Result.Success(success.Data[0]), x =>
                {
                    _logger.LogError("Request failure path: {@Path}\nwith error: {@Error}", x.Code, x.Message);
                    return Result.Failure<ConfirmInvoiceResponse>(x);
                });

        public async Task<Result<AccountMoveResponse>> CreateAccountMove(CreateAccountMoveRequest request) =>
            await CallAsync<AccountMoveResponse>(request).Match(
                success => Result.Success(success.Data[0]),
                Result.Failure<AccountMoveResponse>);

        public async Task<Result<SaleOrderResponse>> GetSaleOrder(string refId) =>
            await CallAsync<SaleOrderResponse>(new GetSaleOrderRequest { RefId = refId }).Match(
                success => Result.Success(success.Data[0]), x =>
                {
                    _logger.LogError("Request failure path: {@Path}\nwith error: {@Error}", x.Code, x.Message);
                    return Result.Failure<SaleOrderResponse>(x);
                });

        public async Task<Result<CreateOrUpdatePaymentResponse>> CreateOrUpdatePayment(CreateOrUpdatePaymentRequest request) =>
            await CallAsync<CreateOrUpdatePaymentResponse>(request).Match(
                success => Result.Success(success.Data[0]), x =>
                {
                    _logger.LogError("Request failure path: {@Path}\nwith error: {@Error}", x.Code, x.Message);
                    return Result.Failure<CreateOrUpdatePaymentResponse>(x);
                });

        public Task<Result> GetPartner()
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreateSaleOrder()
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreatePartner()
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdatePartner()
        {
            throw new NotImplementedException();
        }

        private async Task<Result<OdooResponse<T>>> CallAsync<T>(IOdooRequest request) where T : class
        {
            //log request.getpath()
            _logger.LogInformation("Request path: {@Path}", request.GetPath());
            var restRequest = new RestRequest(request.GetPath(), Method.Get);
            var response = await _restClient.ExecuteAsync<OdooResponse<T>>(restRequest);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Result.Success(response.Data!).Ensure(x => x != null, new Error(request.GetPath(), response.ErrorMessage!)),
                _ => Result.Failure<OdooResponse<T>>(new Error(request.GetPath(), response.Content ?? response.ErrorMessage!))
            };
        }
    }
}
