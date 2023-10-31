using ImporterInvoice.Shared.Commands;
namespace ImporterInvoice.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
 