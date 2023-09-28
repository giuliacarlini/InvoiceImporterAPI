using ControleFinanceiroAPI.Data;
using ControleFinanceiroAPI.Model;
using ControleFinanceiroAPI.Repositories.Interface;
using Dapper;

namespace ControleFinanceiroAPI.Repositories
{
    public class FaturaRepository : IFaturaRepository
    {
        private DbSession _session;

        public FaturaRepository(DbSession session)
        {
            _session = session;
        }

        public int Save(Fatura fatura)
        {
            return _session.Connection.QuerySingle<int>("INSERT INTO [Fatura] OUTPUT INSERTED.IdFatura " +
                " VALUES(@IdOrigem, @Vencimento, @DataHoraCadastro)", fatura, _session.Transaction);
        }
    }
}
