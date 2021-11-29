using System;

namespace Application
{
    public class ApplicationUser {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Password { get; set; }

        public ApplicationUser() {}

        public ApplicationUser(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            IsEmailConfirmed = false;
            Password = password;
        }

        public static ApplicationUser New(string username, string email, string password) {
            return new ApplicationUser(username, email, password);
        }

        
    }
}