﻿using ImportadorFatura.Domain.Enum;
using ImportadorFatura.Domain.ValueObjects;
using ImportadorFatura.Shared.Entities;

namespace ImportadorFatura.Domain.Entities
{
    public class Fatura : Entity
    {
        private List<Lancamento> _lancamentos;

        public Fatura(ETipoImportacao tipoImportacao, DateTime vencimento, CaminhoArquivo caminhoArquivo)
        {
            TipoImportacao = tipoImportacao;
            Vencimento = vencimento;
            DataHoraCadastro = DateTime.Now;
            CaminhoArquivo = caminhoArquivo;
            _lancamentos = new List<Lancamento>();

            AddNotifications(caminhoArquivo);      
        }

        public ETipoImportacao TipoImportacao { get; private set; }

        public DateTime Vencimento { get; private set; }

        public DateTime DataHoraCadastro { get; private set; }

        public CaminhoArquivo CaminhoArquivo { get; private set; }

        public IReadOnlyCollection<Lancamento>? Lancamentos { get { return _lancamentos.ToArray(); } }

        public void AdicionarLancamento(Lancamento lancamento)
        {
            _lancamentos.Add(lancamento);
        }

        public void LerArquivoCSV()
        {
            List<string> arquivo = File.ReadLines(CaminhoArquivo.Caminho).ToList();

            foreach (var linhas in arquivo.Skip(1))
            {
                var lancamento = new Lancamento(TipoImportacao, linhas);
                AdicionarLancamento(lancamento);
            }            
        }
    }
}