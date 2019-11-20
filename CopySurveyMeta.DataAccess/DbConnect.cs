using System;
using System.Data;
using System.Data.SqlClient;

namespace CopySurveyMeta.DataAccess
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public class DbConnect : IDisposable
    {
        private IDbConnection _connection;

        public DbConnect(string connection)
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(connection);
            } 
        }

        //private IDbTransaction _transaction;

        public IDbConnection GetConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        private void dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Close();
                }
            }
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

         
    }
}