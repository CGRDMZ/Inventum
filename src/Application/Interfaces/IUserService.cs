using System;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.Interfaces
{
    public interface IUserService {
        Task<Guid> CreateNewUser(CreateNewUserCommand command);
        Task<ApplicationUser> GetApplicationUser(Guid id);
    }
}