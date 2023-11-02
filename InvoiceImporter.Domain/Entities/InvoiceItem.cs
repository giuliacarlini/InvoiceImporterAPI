using ImporterInvoice.Domain.Shared.Entities;
using Flunt.Validations;
using System.Globalization;
using InvoiceImporter.Domain.Enum;

namespace InvoiceImporter.Domain.Entities
{
    public class InvoiceItem : Entity
    {
        public const string InvalidDate = "A Data de Lançamento está muito antiga";
        public const string InvalidCategory = "A Categoria deve ser preenchida";
        public const string InvalidDescription = "A Descrição deve ser preenhida";
        public const string InvalidValue = "O Valor deve ser maior que zero";

        public InvoiceItem() { }

        public DateTime Date { get; private set; }
        public string Category { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Value { get; private set; }
        public string CurrentyInstallments { get; private set; } = string.Empty;
        public string TotalInstallments { get; private set; } = string.Empty;

        public InvoiceItem(EImportType tipoImportacao, string linha)
        {
            switch (tipoImportacao)
            {
                case EImportType.Nubank:
                    var lineSplitNu = linha.Split(",");

                    var data = DateTime.Parse(ReadRegister(lineSplitNu, 0));
                    Date = data;

                    Category = ReadRegister(lineSplitNu, 1);
                    Description = ReadRegister(lineSplitNu, 2);

                    if (ConvertDecimal(ReadRegister(lineSplitNu, 3), out decimal valor))
                    {
                        Value = valor;
                    }

                    SearchInstallments(ReadRegister(lineSplitNu, 2), out var parcela, out var totalParcela);
                    CurrentyInstallments = parcela;
                    TotalInstallments = totalParcela;

                    break;
                default:
                    AddNotification("TipoImportacao", "O Tipo Importação não foi implementado!");
                    break;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Date.Date, new DateTime(2020, 01, 01), "Data", string.Concat(InvalidDate, ": ", linha))
                .HasMinLen(Category, 1, "Categoria", string.Concat(InvalidCategory, ": ", linha))
                .HasMinLen(Description, 1, "Descricao", string.Concat(InvalidDescription, " ", linha))
                .IsTrue(Value > 0, "Valor", string.Concat(InvalidValue, " ", linha))
                );
        }

        private static bool ConvertDecimal(string originalValue, out decimal value)
        {
            return decimal.TryParse(
                                    originalValue,
                                    NumberStyles.Number,
                                    CultureInfo.InvariantCulture,
                                    out value);
        }

        private static string ReadRegister(string[] lineSplitNu, int position)
        {
            return position < lineSplitNu.Length ? lineSplitNu[position].Trim() : "";
        }

        private void SearchInstallments(string descriptionString, out string currentInstallments, out string totalInstallments)
        {

            if (descriptionString.IndexOf("/") > 0)
            {
                string[] retornoSplit = descriptionString.Split(' ');

                foreach (string s in retornoSplit)
                {
                    if (s.Contains('/'))
                    {
                        totalInstallments = s.Substring(s.IndexOf("/") + 1, new string(s.Reverse().ToArray()).IndexOf("/"));
                        currentInstallments = s.Substring(0, s.IndexOf("/"));

                        return;
                    }
                }
            }

            currentInstallments = "0";
            totalInstallments = "0";
        }
    }
}
