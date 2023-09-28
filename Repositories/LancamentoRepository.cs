using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Model;
using ControleFinanceiroAPI.Repositories.Interface;
using Dapper;

namespace ControleFinanceiroAPI.Repositories
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
            _session.Connection.Execute("INSERT INTO [Lancamento] VALUES(@Data, @Categoria, @Descricao, @Valor, @Parcelado, @Parcela, @TotalParcela, @idImportacao)", lancamento, _session.Transaction);


        }
    }
}
