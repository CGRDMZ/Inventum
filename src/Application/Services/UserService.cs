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

        public async Task<Guid> CreateNewUser(CreateNewUserCommand command)
        {
            ApplicationUser appUser = ApplicationUser.New(command.Username, command.Email, command.Password);
            _applicationUserRepository.CreateNewApplicationUser(appUser);

            User domainUser = User.New(appUser.Id, command.Username);
            await _userRepository.AddAsync(domainUser);

            return domainUser.UserId;
        }

        public Task<ApplicationUser> GetApplicationUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}