using System.Collections.Generic;
using Application;
using Application.Interfaces;

namespace Infrastructure
{
    public class InMemoryApplicationUserRepository : IApplicationUserRepository
    {
        private Dictionary<string, ApplicationUser> users;

        public InMemoryApplicationUserRepository() {
            users = new Dictionary<string, ApplicationUser>();
        }
        
        public ApplicationUser CreateNewApplicationUser(ApplicationUser user)
        {
            users[user.Username] = user;
            return user;
        }

        public ApplicationUser FindApplicationUserByUsername(string username)
        {
            return users.ContainsKey(username) ? users[username] : null;
        }
    }
}