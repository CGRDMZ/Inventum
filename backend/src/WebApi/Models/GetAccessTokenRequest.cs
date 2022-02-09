using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class GetAccessTokenRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}