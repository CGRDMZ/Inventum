using System;

namespace Application
{
    public class ApplicationUser {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Password { get; set; }

        private ApplicationUser() {}

        public ApplicationUser(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            IsEmailConfirmed = false;
            Password = password;
        }
    }
}