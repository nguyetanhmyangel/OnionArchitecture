using OnionArchitecture.Application.Interfaces.Services;
using System;

namespace OnionArchitecture.Infrastructure.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}