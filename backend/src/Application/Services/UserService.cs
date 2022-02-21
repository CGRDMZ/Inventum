using System;
using System.Threading.Tasks;
using Application.Commands;
using Application.Interfaces;
using Domain;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private IApplicationUserRepository _applicationUserRepository;
        private IUserRepository _userRepository;
        public UserService(IApplicationUserRepository applicationUserRepository, IUserRepository userRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultWrapper<Guid>> CreateNewUser(CreateNewUserCommand command)
        {
            if(await _applicationUserRepository.FindApplicationUserExistsByUsername(command.Username)) {
                return new ResultWrapper<Guid>().AddError("User with this name exists already.");
            }
            ApplicationUser appUser = ApplicationUser.New(command.Username, command.Email, command.Password);
            var result = await _applicationUserRepository.CreateNewApplicationUser(appUser);
            if (!result.Success) {
                return result;
            }

            User domainUser = User.New(result.Data, command.Username);
            await _userRepository.AddAsync(domainUser);

            return result;
        }

        public Task<ApplicationUser> GetApplicationUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}