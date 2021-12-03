using System.Collections.Generic;
using System.Threading.Tasks;
using OnionArchitecture.Application.DTOs.Logs;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId);

        Task AddLogAsync(string action, string userId);
    }
}