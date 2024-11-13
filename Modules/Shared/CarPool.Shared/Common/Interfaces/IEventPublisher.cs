namespace CarPool.Shared.Events.Common.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : class;
}