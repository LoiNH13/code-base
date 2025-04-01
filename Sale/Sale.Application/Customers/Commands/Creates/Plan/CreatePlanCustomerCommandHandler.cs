using Application.Core.Abstractions.Data;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Odoo.Domain.Core.Errors;
using Odoo.Domain.Entities;
using Odoo.Domain.Repositories;
using Sale.Application.Core.Authentication;
using Sale.Domain.Entities.Customers;
using Sale.Domain.Repositories;
using Sale.Domain.Services;

namespace Sale.Application.Customers.Commands.Creates.Plan;

internal sealed class CreatePlanCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IUserIdentifierProvider userIdentifierProvider,
    IOdooStateRepository odooStateRepository)
    : ICommandHandler<CreatePlanCustomerCommand, Result>
{
    public async Task<Result> Handle(CreatePlanCustomerCommand request, CancellationToken cancellationToken)
    {
        Maybe<ResCountryState> mbResCountryState = await odooStateRepository.GetByIdAndIncludeAllAsync(request.CityId);
        if (mbResCountryState.HasNoValue)
        {
            return Result.Failure(OdooDomainErrors.States.NotFound);
        }

        Maybe<ResDistrict> mbResDistrict =
            mbResCountryState.Value.ResDistricts.FirstOrDefault(x => x.Id == request.DistrictId)!;
        if (mbResDistrict.HasNoValue) return Result.Failure(OdooDomainErrors.Districts.NotFound);

        Maybe<int> mbCount = await customerRepository.PlanNewCustomerCount();
        string code =
            PlanNewCustomerServices.GenerateCustomerCode(mbResCountryState.Value.Name, mbResDistrict.Value.Name,
                mbCount);
        string name =
            PlanNewCustomerServices.GenerateCustomerName(mbResCountryState.Value.Name, mbResDistrict.Value.Name,
                mbCount);

        Result<Customer> rsCustomer = CustomerServices.CreatePlanCustomer(code,
            name,
            request.CityId,
            request.DistrictId,
            request.WardId,
            userIdentifierProvider.UserId);
        if (rsCustomer.IsSuccess)
        {
            customerRepository.Insert(rsCustomer.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return rsCustomer;
    }
}