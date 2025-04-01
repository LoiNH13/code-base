namespace Sale.Domain.Core.Abstractions;

public interface IMustHaveCreateUser
{
    public Guid CreateByUser { get; init; }
}
