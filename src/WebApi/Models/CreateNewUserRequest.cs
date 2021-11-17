using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CreateNewUserRequest
    {
        [Required]
        [MinLength(5)]
        public string Username { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
        
    }
}