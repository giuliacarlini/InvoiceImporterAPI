using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Entities;
using Dapper;

namespace ControleFinanceiro.Infra.Repositories
{
    public class ImportacaoRepository : IImportacaoRepository
    {
        private DbSession _session;

        public ImportacaoRepository(DbSession session)
        {
            _session = session;
        }
        public Importacao Save(Importacao importacao)
        {
            int id = _session.Connection.QuerySingle<int>(
                "INSERT INTO [Importacao] " +
                "   OUTPUT INSERTED.IdImportacao " +
                " VALUES (@Data, " +
                "         @Categoria, " +
                "         @Descricao, " +
                "         @Valor, " +
                "         @DataHoraImportacao, " +
                "         @idOrigemImportacao, " +
                "         @IdFatura)",
               importacao, _session.Transaction);

            importacao.IdImportacao = id;

            return importacao;
        }
    }
}
