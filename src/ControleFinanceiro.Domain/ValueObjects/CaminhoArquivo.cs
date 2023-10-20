using Flunt.Notifications;
using Flunt.Validations;

namespace ControleFinanceiro.Domain.ValueObjects
{
    public class CaminhoArquivo: Notifiable
    {
        public CaminhoArquivo(string caminhoArquivo) 
        {
            Nome = caminhoArquivo.Split('\\').Last();
            Diretorio = caminhoArquivo.Replace(Nome, "");

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Directory.Exists(Diretorio), "CaminhoArquivo.Diretorio", "Diretório de arquivo não encontrado")
                .IsTrue(File.Exists(Diretorio + Nome), "CaminhoArquivo.Nome", "Arquivo não encontrado")
                .IsTrue(Path.GetExtension(Diretorio + Nome) == ".csv", "CaminhoArquivo.Nome", "A extensão de arquivo é inválida para a importação")
            );
        }

        public string Diretorio { get; private set; }
        public string Nome { get; private set; }
        public string Caminho { get { return Diretorio + Nome; } }
    }
}