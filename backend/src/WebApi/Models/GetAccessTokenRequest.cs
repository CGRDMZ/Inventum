using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class GetAccessTokenRequest
{

    [Required]
    [MinLength(5)]
    public string Username { get; init; }

    [Required]
    public string Password { get; init; }
}