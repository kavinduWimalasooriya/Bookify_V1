using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews.Events;

public record ReviewCreateDomainEvent(Guid ReviewId) : IDomainEvent;