using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Database
{
    public class TransactionManager : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public TransactionManager(SqlConnection connection)
        {
            _connection = connection;
        }

        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }
        
        public SqlTransaction GetTransaction()
        {
            return _transaction;
        }
        public bool TransactionExists()
        {
            return _transaction != null;
        }
        public void CommitTransaction()
        {
            _transaction.Commit();
        }
        
        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}