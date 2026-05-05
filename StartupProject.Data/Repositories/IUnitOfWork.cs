using System;
using System.Threading.Tasks;

namespace StartupProject.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        void Commit();
    }
}