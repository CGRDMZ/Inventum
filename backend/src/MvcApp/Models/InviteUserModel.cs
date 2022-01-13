using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class InviteUserModel
    {
        [MaxLength(10)]
        public string Username { get; init; }
    }
}