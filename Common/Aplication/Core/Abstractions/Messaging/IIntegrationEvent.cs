﻿using MediatR;

namespace Application.Core.Abstractions.Messaging;

/// <summary>
/// Represents the marker interface for an integration event.
/// </summary>
public interface IIntegrationEvent : INotification
{
}
