using Flunt.Notifications;
using Flunt.Validations;
using ImportadorFatura.Domain.Enum;
using ImportadorFatura.Domain.ValueObjects;
using ImportadorFatura.Shared.Commands;

namespace ImportadorFatura.Domain.Commands
{
    public class CriarFaturaCommand : Notifiable, ICommand
    {
        public ETipoImportacao TipoImportacao { get; set; }
        public DateTime Vencimento { get; set; }
        public string CaminhoArquivo { get; set; } = string.Empty;

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Path.Exists(CaminhoArquivo), "CaminhoArquivo", "O Caminho está inválido!")
                .IsNotNull(TipoImportacao, "TipoExportacao", "O Tipo da Exportação está inválido!")
                .IsBetween(Vencimento, new DateTime(2020,1,1), DateTime.Now, "Vencimento", "O Vencimento está inválido!"));
        }
    }
}
