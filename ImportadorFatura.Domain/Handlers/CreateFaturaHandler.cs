using Flunt.Notifications;
using ImportadorFatura.Domain.Adapters.Repository;
using ImportadorFatura.Domain.Commands;
using ImportadorFatura.Domain.Entities;
using ImportadorFatura.Domain.ValueObjects;
using ImportadorFatura.Shared.Commands;
using ImportadorFatura.Shared.Handlers;

namespace ImportadorFatura.Domain.Handlers
{
    public class CriarFaturaHandler : Notifiable, IHandler<CriarFaturaCommand>
    {
        private readonly IFaturaRepository _faturaRepository;

        public CriarFaturaHandler(IFaturaRepository faturaRepository)
        {
            _faturaRepository = faturaRepository;
        }

        public ICommandResult Handle(CriarFaturaCommand command)
        {
            command.Validar();

            if (command.Invalid)
                return new CommandResult() { Sucesso = false, Mensagem = command.Notifications.Last().Message };

            if (_faturaRepository.BuscarFatura(command.CaminhoArquivo))
                AddNotification("CaminhoArquivo", "O Arquivo já foi adicionado anteriormente.");

            if (_faturaRepository.BuscarFatura(command.Vencimento, command.TipoImportacao))
                AddNotification("Arquivo", "Já foi importado um arquivo para mesma data de Vencimento e tipo");

            var caminhoArquivo = new CaminhoArquivo(command.CaminhoArquivo);

            var fatura = new Fatura(command.TipoImportacao, command.Vencimento, caminhoArquivo);         

            fatura.LerArquivoCSV();

            AddNotifications(fatura, caminhoArquivo);

            if (Invalid)
                return new CommandResult() { Sucesso = false, Mensagem = "Não foi possível importar a fatura." };

            return new CommandResult() { Sucesso = true, Mensagem = "" };
        }
    }
}
