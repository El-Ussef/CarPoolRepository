namespace Identity.Core.Application.Contracts;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : class;
}