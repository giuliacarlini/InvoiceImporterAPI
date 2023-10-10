using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.Services;

namespace ControleFinanceiro.Domain.Adapters
{
    public class ImportarService : IImportarService
    {

        private readonly IFaturaRepository _faturaRepository;
        private readonly IImportacaoRepository _importacaoRepository;
        private readonly ILancamentoRepository _lancamentosRepository;

        public ImportarService(
                               IFaturaRepository faturaRepository,
                               IImportacaoRepository importacaoRepository,
                               ILancamentoRepository lancamentosRepository)
        {

            _faturaRepository = faturaRepository;
            _importacaoRepository = importacaoRepository;
            _lancamentosRepository = lancamentosRepository;
        }

        public int ImportarArquivo(string CaminhoArquivo, DateTime Vencimento, TipoImportacao TipoImportacao)
        {
            if (_faturaRepository.BuscarFaturaPorNomeArquivo(Path.GetFileName(CaminhoArquivo)))
                throw new Exception("Arquivo já importado!");

            try
            {
                var Fatura = AdicionarFatura(Vencimento, TipoImportacao, Path.GetFileName(CaminhoArquivo));

                if (Fatura == null)
                    throw new Exception("Erro ao adicionar fatura!");

                var data = File.ReadLines(CaminhoArquivo);
                var listaImportacao = TransformarLinhasEmObjeto(data, TipoImportacao, Fatura.IdFatura);

                foreach (var importacao in listaImportacao)
                {
                    if (DateTime.TryParse(importacao.Data, out DateTime dataConvertida) == false)
                        continue;

                    var idImportacao = AdicionarImportacao(importacao).IdImportacao;

                    if (idImportacao > 0)
                    {
                        AdicionarLancamento(importacao, idImportacao);
                    }
                }

                return Fatura.IdFatura;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private List<Importacao> TransformarLinhasEmObjeto(IEnumerable<string> data, TipoImportacao tipoImportacao, int idFatura)
        {
            List<Importacao> listaImportacao = new();

            foreach (var line in data)
            {
                switch (tipoImportacao)
                {
                    case TipoImportacao.Nubank:
                        var lineSplitNu = line.Split(",");
                        listaImportacao.Add(new Importacao(lineSplitNu[0], lineSplitNu[1], lineSplitNu[2], lineSplitNu[3], (int)tipoImportacao, idFatura));
                        break;
                    case TipoImportacao.C6Bank:
                        var lineSplitC6 = line.Split(";");
                        listaImportacao.Add(new Importacao(lineSplitC6[0], lineSplitC6[3], lineSplitC6[4] + " " + lineSplitC6[5], lineSplitC6[8], (int)tipoImportacao, idFatura));

                        break;
                }
            }

            return listaImportacao;
        }

        private void AdicionarLancamento(Importacao importacao, int idImportacao)
        {
            _lancamentosRepository.Save(new Lancamento()
            {
                Data = DateTime.Parse(importacao.Data),
                Categoria = importacao.Categoria,
                Descricao = importacao.Descricao,
                Valor = decimal.Parse(importacao.Valor.Replace(".", ","), System.Globalization.NumberStyles.Currency),
                Parcelado = LocalizarParcela(importacao, false) != "",
                Parcela = LocalizarParcela(importacao, false),
                TotalParcela = LocalizarParcela(importacao, true),
                IdImportacao = idImportacao
            });
        }

        private Importacao AdicionarImportacao(Importacao importacao)
        {
            return _importacaoRepository.Save(importacao);
        }

        private Fatura AdicionarFatura(DateTime vencimento, TipoImportacao tipoImportacao, string nomeArquivo)
        {
            return _faturaRepository.Save(new Fatura(0, (int)tipoImportacao, vencimento, DateTime.Now, nomeArquivo));
        }

        private string LocalizarParcela(Importacao file, bool TotalParcela)
        {
            try
            {
                if (file.Descricao.IndexOf("/") > 0)
                {
                    string[] retornoSplit = file.Descricao.Split(' ');

                    foreach (string s in retornoSplit)
                    {
                        if (s.Contains("/"))
                        {
                            var resultado = TotalParcela ? s.Substring(s.IndexOf("/") + 1, new string(s.Reverse().ToArray()).IndexOf("/")) : s.Substring(0, s.IndexOf("/"));

                            return int.Parse(resultado).ToString();
                        }
                    }
                }

                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
