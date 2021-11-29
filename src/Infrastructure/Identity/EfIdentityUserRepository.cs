using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Infrastructure.Identity
{
    public class EfIdentityUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public EfIdentityUserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultWrapper<Guid>> CreateNewApplicationUser(ApplicationUser user)
        {
            var newAppUser = new AppUser()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Username
            };
            var result = await _userManager.CreateAsync(newAppUser, user.Password);


            return new ResultWrapper<Guid>() {
                Errors = result.Errors.Select(e => e.Description).ToList(),
                Data = newAppUser.Id
            };

        }

        public async Task<bool> FindApplicationUserExistsByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

        public async Task<ApplicationUser> FindApplicationUserById(string id) {
            var appuser = await _userManager.FindByIdAsync(id);

            return ApplicationUser.New(appuser.UserName, appuser.Email, null);
        } 
    }
}