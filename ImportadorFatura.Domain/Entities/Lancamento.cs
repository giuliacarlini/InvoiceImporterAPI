using ImportadorFatura.Domain.Enum;
using ImportadorFatura.Shared.Entities;
using Flunt.Validations;
using System.Globalization;

namespace ImportadorFatura.Domain.Entities
{
    public class Lancamento : Entity
    {
        public const string DataInvalida = "A Data de Lançamento está muito antiga";
        public const string CategoriaInvalida = "A Categoria deve ser preenchida";
        public const string DescriacaoInvalida = "A Descrição deve ser preenhida";
        public const string ValorInvalido = "O Valor deve ser maior que zero";

        public Lancamento(ETipoImportacao tipoImportacao, string linha)
        {
            IdLancamento = Guid.NewGuid();

            switch (tipoImportacao)
            {
                case ETipoImportacao.Nubank:
                    var lineSplitNu = linha.Split(",");

                    var data = DateTime.Parse(LerRegistro(lineSplitNu, 0));
                    Data = data;

                    Categoria = LerRegistro(lineSplitNu, 1);
                    Descricao = LerRegistro(lineSplitNu, 2);

                    if (ConverterDecimal(LerRegistro(lineSplitNu, 3), out decimal valor))
                    {
                        Valor = valor;
                    }

                    LocalizarParcela(LerRegistro(lineSplitNu, 2), out var parcela, out var totalParcela);
                    Parcela = parcela;
                    TotalParcela = totalParcela;

                    break;
                default:
                    AddNotification("TipoImportacao", "O Tipo Importação não foi implementado!");
                    break;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Data.Date, new DateTime(2020, 01, 01), "Data", string.Concat(DataInvalida, ": ", linha))
                .HasMinLen(Categoria, 1, "Categoria", string.Concat(CategoriaInvalida, ": ", linha))
                .HasMinLen(Descricao, 1, "Descricao", string.Concat(DescriacaoInvalida, " ", linha))
                .IsTrue(Valor > 0, "Valor", string.Concat(ValorInvalido, " ", linha))
                );
        }

        public Guid IdLancamento { get; private set; }
        public DateTime Data { get; private set; }
        public string Categoria { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public bool? Parcelado { get; private set; }
        public string Parcela { get; private set; } = string.Empty;
        public string TotalParcela { get; private set; } = string.Empty;

        private static bool ConverterDecimal(string valorOriginal, out decimal valor)
        {
            return decimal.TryParse(
                                    valorOriginal,
                                    NumberStyles.Number,
                                    CultureInfo.InvariantCulture,
                                    out valor);
        }

        private static string LerRegistro(string[] lineSplitNu, int posicao)
        {
            return posicao < lineSplitNu.Length ? lineSplitNu[posicao].Trim() : "";
        }

        private void LocalizarParcela(string descricao, out string parcela, out string totalParcela)
        {

            if (descricao.IndexOf("/") > 0)
            {
                string[] retornoSplit = descricao.Split(' ');

                foreach (string s in retornoSplit)
                {
                    if (s.Contains('/'))
                    {
                        totalParcela = s.Substring(s.IndexOf("/") + 1, new string(s.Reverse().ToArray()).IndexOf("/"));
                        parcela = s.Substring(0, s.IndexOf("/"));

                        return;
                    }
                }
            }

            parcela = "0";
            totalParcela = "0";
        }
    }
}
