using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<bool> FindApplicationUserExistsByUsername(string username);
        Task<ResultWrapper<Guid>> CreateNewApplicationUser(ApplicationUser user);
    }
}