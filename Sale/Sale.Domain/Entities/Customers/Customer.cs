using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Primitives.Maybe;
using Domain.Core.Primitives.Result;
using Domain.Core.Utility;
using Sale.Domain.Core.Abstractions;
using Sale.Domain.Core.Errors;
using Sale.Domain.Entities.Metrics;
using Sale.Domain.Entities.MonthlyReports;
using Sale.Domain.Entities.Users;
using Sale.Domain.Enumerations;
using Sale.Domain.Events;
using Sale.Domain.Repositories;
using Sale.Domain.Services;

namespace Sale.Domain.Entities.Customers;

public sealed class Customer : AggregateRoot, IAuditableEntity, ISoftDeletableEntity, IHaveManagedByUser
{
    public Customer(int? odooRef,
        string name,
        EBusinessType businessType,
        Guid? managedByUserId,
        int visitPerMonth) : base(Guid.NewGuid())
    {
        OdooRef = odooRef;
        Name = name;
        BusinessType = businessType;
        ManagedByUserId = managedByUserId;
        VisitPerMonth = visitPerMonth;
    }

    internal Customer(string name, Guid managedByUserId) : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(managedByUserId, "ManagedByUserId cannot be empty.", nameof(ManagedByUserId));

        Name = name;
        ManagedByUserId = managedByUserId;
    }

    public int? OdooRef { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public EBusinessType BusinessType { get; set; } = EBusinessType.Dealer;

    public Guid? ManagedByUserId { get; private set; }

    public int VisitPerMonth { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public PlanNewCustomer? PlanNewCustomer { get; private set; }

    public List<CustomerTimeFrame> CustomerTimeFrames { get; private set; } = new();

    public List<MonthlyReport> MonthlyReports { get; private set; } = new();

    private Customer()
    {
    }

    public Result Update(string name, User? managedByUserId, int visitPerMonth, EBusinessType businessType)
    {
        Name = name;
        ManagedByUserId = managedByUserId?.Id;
        VisitPerMonth = visitPerMonth;
        BusinessType = businessType;
        return Result.Success();
    }

    //make function RemoveCustomer must check if PlanNewCustomer is null then allow to remove
    public Result RemoveCustomer()
    {
        if (PlanNewCustomer is not null) return Result.Failure(SaleDomainErrors.Customer.PlanNewCustomerCannotRemove);
        return Result.Success();
    }

    public void UpdateManagedByUser(Guid? managedByUserId) => ManagedByUserId = managedByUserId;

    public void UpdateByEvent(string name)
    {
        Name = name;
    }

    public Result<CustomerTimeFrame> FindCustomerTimeFrame(TimeFrame timeFrame)
    {
        Maybe<CustomerTimeFrame> mbCustomerTimeFrame =
            CustomerTimeFrames.Find(x => x.TimeFrameId == timeFrame.Id) ?? default!;
        if (mbCustomerTimeFrame.HasValue) return Result.Success<CustomerTimeFrame>(mbCustomerTimeFrame);

        mbCustomerTimeFrame = new CustomerTimeFrame(this, timeFrame);
        CustomerTimeFrames.Add(mbCustomerTimeFrame);

        return Result.Success<CustomerTimeFrame>(mbCustomerTimeFrame);
    }

    internal Result CreatePlanCustomer(string code, int cityId, int districtId, int? wardId)
    {
        PlanNewCustomer = new PlanNewCustomer(this, code, cityId, districtId, wardId);

        return Result.Success();
    }

    public void CalculateMetricsLogic(int fromConvertMonths)
    {
        MetricServices.ReCalculateMetrics(CustomerTimeFrames
            .Where(x => x.TimeFrame!.ConvertMonths >= fromConvertMonths).ToList());
    }

    //update odoo ref when it null, then call update UpdateIsOpen at plannewcustomer
    public async Task<Result> UpdateOdooRef(int targetOdooRef, string name, ICustomerRepository customerRepository)
    {
        if (OdooRef is not null) return Result.Failure(SaleDomainErrors.Customer.OdooRefAlreadyExist);
        //check odoo ref existed
        if (await customerRepository.OdooRefExisted(targetOdooRef))
            return Result.Failure(SaleDomainErrors.Customer.TargetOdooRefExisted);

        Name = name;
        OdooRef = targetOdooRef;
        //create domain event
        AddDomainEvent(new PlanningCustomerMappingOdooEvent(Id));
        return PlanNewCustomer!.UpdateIsOpen(this, true);
    }

    public void MappingOdooEvent(int currentConvetMonths) => CleanForecastToConvertMonths(currentConvetMonths);

    private void CleanForecastToConvertMonths(int toConvertMonths)
    {
        foreach (var item in CustomerTimeFrames.Where(x => x.TimeFrame!.ConvertMonths < toConvertMonths))
        {
            item.Metrics.ForEach(x => x.ClearForecast());
        }
        MetricServices.ReCalculateMetrics(CustomerTimeFrames);
    }

    internal void UpdateBusinessType(EBusinessType businessType)
    {
        BusinessType = businessType;
    }
}