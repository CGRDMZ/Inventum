
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateBoardRequest
{

    [Required]
    [MinLength(5)]
    public string Name { get; init; }

    public string Color { get; init; }
    
    
}