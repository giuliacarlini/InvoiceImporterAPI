using Flunt.Notifications;
using Flunt.Validations;

namespace InvoiceImporter.Domain.ValueObjects
{
    public class FilePath : Notifiable
    {
        public FilePath(string filePath)
        {
            Name = filePath.Split('\\').Last();
            Path = filePath.Replace(Name, "");

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Directory.Exists(Path), "CaminhoArquivo.Diretorio", "Diretório de arquivo não encontrado")
                .IsTrue(File.Exists(Path + Name), "CaminhoArquivo.Nome", "Arquivo não encontrado")
                .IsTrue(System.IO.Path.GetExtension(Path + Name) == ".csv", "CaminhoArquivo.Nome", "A extensão de arquivo é inválida para a importação")
            );
        }

        public string Path { get; private set; }
        public string Name { get; private set; }
        public override string ToString() { return Path + Name; }
    }
}