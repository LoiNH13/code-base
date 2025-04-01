using Domain.Core.Primitives.Result;
using Sale.Domain.Enumerations;

namespace Sale.Application.Customers.Commands.Creates.Odoo;

public sealed class CreateOdooCustomerCommand : ICommand<Result>
{
    public int OdooRef { get; }

    public EBusinessType BusinessType { get; }

    public int VisitPerMonth { get; }

    public Guid? ManagedByUserId { get; }

    public CreateOdooCustomerCommand(int odooRef, int visitPerMonth, Guid? managedByUserId, EBusinessType businessType)
    {
        OdooRef = odooRef;
        VisitPerMonth = visitPerMonth;
        ManagedByUserId = managedByUserId;
        BusinessType = businessType;
    }
}
