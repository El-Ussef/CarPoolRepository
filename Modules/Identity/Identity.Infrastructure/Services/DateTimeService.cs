using Identity.Core.Application.Contracts;

namespace Identity.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now { get; set; } = DateTime.UtcNow;
}