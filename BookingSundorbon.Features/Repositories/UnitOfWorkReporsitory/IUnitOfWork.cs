using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.UnitOfWorkReporsitory
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction Transaction { get; }
        void BeginTransaction();
        void Commit();
        IDbConnection GetConnection();
        void Rollback();
    }
}
