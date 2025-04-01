using Domain.Core.Primitives.Result;

namespace Sale.Application.Products.Commands.Updates.Status;

public sealed class UpdateProductStatusCommand : ICommand<Result>
{
    public Guid Id { get; }

    public UpdateProductStatusCommand(Guid id) => Id = id;
}
