using ImportadorFatura.Shared.Commands;

namespace ImportadorFatura.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }

        public CommandResult(bool success, string message)
        {
            Sucesso = success;
            Mensagem = message;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;

    }
}
