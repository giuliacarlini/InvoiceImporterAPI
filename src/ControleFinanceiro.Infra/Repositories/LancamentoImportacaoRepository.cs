using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Entities;
using Dapper;

namespace ControleFinanceiro.Infra.Repositories
{
    public class LancamentoImportacaoRepository : ILancamentoImportacaoRepository
    {
        private DbSession _session;

        public LancamentoImportacaoRepository(DbSession session)
        {
            _session = session;
        }
        public LancamentoImportacao Adicionar(LancamentoImportacao importacao)
        {
            int id = _session.Connection.QuerySingle<int>(
                "INSERT INTO [LancamentoImportacao] " +
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
