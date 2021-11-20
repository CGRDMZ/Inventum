using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class CreateNewUserRequestModel
    {
        [Required]
        [MinLength(5)]
        public string Username { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [MinLength(8)]
        public string Password { get; init; }
        
    }
}