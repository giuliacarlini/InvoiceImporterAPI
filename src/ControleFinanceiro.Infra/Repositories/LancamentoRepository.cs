using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Entities;
using Dapper;

namespace ControleFinanceiro.Infra.Repositories
{
    public class LancamentoRepository : ILancamentoRepository
    {
        private DbSession _session;

        public LancamentoRepository(DbSession session)
        {
            _session = session;
        }
        public void Save(Lancamento lancamento)
        {
            _session.Connection.Query("INSERT INTO [Lancamento] " +
                "   VALUES(@Data, " +
                "          @Categoria, " +
                "          @Descricao, " +
                "          @Valor, " +
                "          @Parcelado, " +
                "          @Parcela, " +
                "          @TotalParcela, " +
                "          @idImportacao)", lancamento, _session.Transaction);
        }
    }
}
