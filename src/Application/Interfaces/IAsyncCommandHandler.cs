using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAsyncCommandHandler<Command, Dto> where Command: ICommand {
        Task<Dto> Handle(Command command);
    }
}