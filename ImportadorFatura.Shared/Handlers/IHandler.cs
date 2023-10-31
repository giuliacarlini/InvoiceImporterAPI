using ImportadorFatura.Shared.Commands;
namespace ImportadorFatura.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
 