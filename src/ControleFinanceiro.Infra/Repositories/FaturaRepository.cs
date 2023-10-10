﻿using ControleFinanceiro.Domain.Adapters;
using ControleFinanceiro.Domain.Data;
using ControleFinanceiro.Domain.Entities;
using Dapper;

namespace ControleFinanceiro.Infra.Repositories
{
    public class FaturaRepository : IFaturaRepository
    {
        private DbSession _session;

        public FaturaRepository(DbSession session)
        {
            _session = session;
        }

        public Fatura Save(Fatura fatura)
        {
            var id = _session.Connection.QuerySingle<int>(
                    "INSERT INTO [Fatura] " +
                    "   OUTPUT INSERTED.IdFatura " +
                    "VALUES(@IdOrigem, " +
                    "       @Vencimento, " +
                    "       @DataHoraCadastro," +
                    "       @NomeArquivo)",
                fatura,
                _session.Transaction);

            fatura.IdFatura = id;

            return fatura;
        }

        public bool BuscarFaturaPorNomeArquivo(string nomeArquivo)
        {
            var parametros = new
            {
                nomeArquivo
            };

            return _session.Connection.Query<Fatura>(
                        "SELECT " +
                        "   IdFatura as IdFatura, " +
                        "   IdOrigem as IdOrigem, " +
                        "   Vencimento as Vencimento, " +
                        "   DataHoraCadastro as DataHoraCadastro, " +
                        "   NomeArquivo as NomeArquivo " +
                        "FROM [Fatura] " +
                        "where NomeArquivo = @nomeArquivo",
                    parametros,
                    _session.Transaction).Any();
        }
    }
}