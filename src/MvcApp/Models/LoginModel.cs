using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; init; }
        
        [Required]
        public string Password { get; init; }
        
        
    }
}