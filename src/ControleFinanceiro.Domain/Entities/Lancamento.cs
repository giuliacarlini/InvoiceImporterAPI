using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.Domain.Entities
{
    public class Lancamento
    {
        public Lancamento(DateTime data, string categoria, string descricao, decimal valor, bool? parcelado, string parcela, string totalParcela, LancamentoImportacao lancamentoImportacao)
        {
            IdLancamento = Guid.NewGuid();
            Data = data;
            Categoria = categoria;
            Descricao = descricao;
            Valor = valor;
            Parcelado = parcelado;
            Parcela = parcela;
            TotalParcela = totalParcela;
            LancamentoImportacao = lancamentoImportacao;
        }

        public Guid IdLancamento { get; private set; }
        public DateTime Data { get; private set; }
        public string Categoria { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public bool? Parcelado { get; private set; }
        public string Parcela { get; private set; } = string.Empty;
        public string TotalParcela { get; private set; } = string.Empty;
        public Guid IdImportacao { get; private set; }
        public virtual LancamentoImportacao? LancamentoImportacao { get; set; }

        public Lancamento(LancamentoImportacao lancamentoImportacao, string line, TipoImportacao tipoImportacao)
        {
            switch (tipoImportacao)
            {
                case TipoImportacao.Nubank:
                    var lineSplitNu = line.Split(",");

                    IdLancamento = Guid.NewGuid();
                    Data = DateTime.Parse(lineSplitNu[0]);
                    Categoria = lineSplitNu[1];
                    Descricao = lineSplitNu[2];
                    Valor = decimal.Parse(lineSplitNu[3].Replace(".", ","), System.Globalization.NumberStyles.Currency);
                    Parcelado = LocalizarParcela(lineSplitNu[2], false) != "";
                    Parcela = LocalizarParcela(lineSplitNu[2], false);
                    TotalParcela = LocalizarParcela(lineSplitNu[2], true);

                    break;
                default:
                    throw new Exception("Tipo de importação inválida");
            }

            IdImportacao = lancamentoImportacao.IdImportacao;
            LancamentoImportacao = lancamentoImportacao;
        }

        private static string LocalizarParcela(string descricao, bool TotalParcela)
        {
            try
            {
                if (descricao.IndexOf("/") > 0)
                {
                    string[] retornoSplit = descricao.Split(' ');

                    foreach (string s in retornoSplit)
                    {
                        if (s.Contains("/"))
                        {
                            var resultado = TotalParcela ? s.Substring(s.IndexOf("/") + 1, new string(s.Reverse().ToArray()).IndexOf("/")) : s.Substring(0, s.IndexOf("/"));

                            return int.Parse(resultado).ToString();
                        }
                    }
                }
            }
            catch
            {
                return "";
            }

            return "";
        }
    }
}
