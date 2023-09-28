using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Model;
using ControleFinanceiroAPI.Repositories.Interface;
using Dapper;

namespace ControleFinanceiroAPI.Repositories
{
    public class ImportacaoRepository : IImportacaoRepository
    {
        private DbSession _session;

        public ImportacaoRepository(DbSession session)
        {
            _session = session;
        }
        public int Save(Importacao importacao)
        {
            return _session.Connection.QuerySingle<int>("INSERT INTO [Importacao] OUTPUT INSERTED.IdImportacao " +
                " VALUES (@date, @category, @title, @amount, @DataHoraImportacao, @idOrigemImportacao, @idFatura)",
               importacao, _session.Transaction);
        }
    }
}
