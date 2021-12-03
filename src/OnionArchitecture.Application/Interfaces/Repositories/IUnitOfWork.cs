using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellationToken);

        Task Rollback();
    }
}