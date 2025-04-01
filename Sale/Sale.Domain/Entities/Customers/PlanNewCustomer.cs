using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Result;
using Sale.Domain.Core.Errors;

namespace Sale.Domain.Entities.Customers;

public sealed class PlanNewCustomer : Entity, IAuditableEntity, ISoftDeletableEntity
{
    internal PlanNewCustomer(Customer customer, string code, int cityId, int? districtId, int? wardId)
    {
        CustomerId = customer.Id;
        CityId = cityId;
        DistrictId = districtId;
        WardId = wardId;
        Code = code;
        IsOpen = false;
    }

    public Guid CustomerId { get; init; }

    public string Code { get; private set; } = string.Empty;

    public int CityId { get; private set; }

    public int? DistrictId { get; private set; }

    public int? WardId { get; private set; }

    public bool IsOpen { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public Customer? Customer { get; }

    private PlanNewCustomer() { }

    //update is open status, customer must have odoo ref then change to open
    public Result UpdateIsOpen(Customer customer, bool isOpen)
    {
        if (isOpen && customer.OdooRef == null)
        {
            return Result.Failure(SaleDomainErrors.PlannewCustomer.MustHaveOdooRef);
        }
        IsOpen = isOpen;
        return Result.Success();
    }

}
