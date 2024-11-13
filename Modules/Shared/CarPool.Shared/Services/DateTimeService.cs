using CarPool.Shared.Events.Interfaces;

namespace CarPool.Shared.Events.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now { get; set; } = DateTime.UtcNow;
}