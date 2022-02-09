using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CreateNewUserRequest
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        
    }
}