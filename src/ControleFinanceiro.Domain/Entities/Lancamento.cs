using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.Domain.Entities
{
    public class Lancamento
    {
        public Guid IdLancamento { get; private set; }
        public DateTime Data { get; private set; }
        public string Categoria { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public bool? Parcelado { get; private set; }
        public string Parcela { get; private set; } = string.Empty;
        public string TotalParcela { get; private set; } = string.Empty;
        public Guid IdImportacao { get; private set; }

        public Lancamento(TipoImportacao tipoImportacao, string linha)
        {
            IdLancamento = Guid.NewGuid();

            switch (tipoImportacao)
            {
                case TipoImportacao.Nubank:
                    var lineSplitNu = linha.Split(",");
                    
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
