using ImporterInvoice.Domain.Shared.Commands;

namespace ImporterInvoice.Domain.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
 