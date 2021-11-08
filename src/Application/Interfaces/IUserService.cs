using Application.Commands;

namespace Application.Interfaces
{
    public interface IUserService {
        bool CreateNewUser(CreateNewUserCommand command);
    }
}