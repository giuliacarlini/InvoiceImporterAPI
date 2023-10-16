using Microsoft.Data.SqlClient;
using System.Data;

namespace ControleFinanceiro.Domain.Data
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        public DbSession()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }
}
