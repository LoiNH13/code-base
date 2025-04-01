﻿using Application.Core.Abstractions.Common;

namespace Infrastructure.Common;

/// <summary>
/// Represents the machine date time service.
/// </summary>
internal sealed class MachineDateTime : IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;

    public int CurrentConvertMonths => UtcNow.Year * 12 + UtcNow.Month;
}
