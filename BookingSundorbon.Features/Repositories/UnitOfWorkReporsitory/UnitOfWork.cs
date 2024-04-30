using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.UnitOfWorkReporsitory
{
    internal class UnitOfWork:IUnitOfWork
    {
        private IDbConnection _dbConnection;
        private IDbTransaction _transaction;
        private bool _disposed = false;
        private readonly string _connectionstring;
        public UnitOfWork(IConfiguration config)
        {
            _connectionstring = config.GetConnectionString("DefaultConnection");
            _dbConnection = new SqlConnection(_connectionstring);
        }

        public IDbTransaction Transaction => _transaction;

        public void BeginTransaction()
        {
            _dbConnection.Open();
            _transaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public IDbConnection GetConnection()
        {
            return _dbConnection;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_dbConnection != null)
                    {
                        _dbConnection.Close();
                        _dbConnection.Dispose();
                        _dbConnection = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}
