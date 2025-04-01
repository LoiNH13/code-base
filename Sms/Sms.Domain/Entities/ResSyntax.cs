using Domain.Core.Abstractions;
using Domain.Core.Primitives;

namespace Sms.Domain.Entities;

public sealed class ResSyntax(
    string syntaxName,
    string? description,
    string syntaxValue,
    string? syntaxRegex,
    string? metadata)
    : Entity(Guid.NewGuid()), IAuditableEntity, ISoftDeletableEntity
{
    public string SyntaxName { get; private set; } = syntaxName;

    public string? Description { get; private set; } = description;

    public string SyntaxValue { get; private set; } = syntaxValue;

    public string? SyntaxRegex { get; private set; } = syntaxRegex;

    public string? Metadata { get; private set; } = metadata;

    public bool Inactive { get; private set; } = true;

    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }
}