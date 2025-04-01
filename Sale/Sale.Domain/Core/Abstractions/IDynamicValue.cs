using Sale.Domain.Enumerations;

namespace Sale.Domain.Core.Abstractions;

public interface IDynamicValue
{
    EBusinessType GetBusinessType();

    string Serialize();
}