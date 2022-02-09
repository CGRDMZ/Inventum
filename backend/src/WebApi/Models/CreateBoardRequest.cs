
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateBoardRequest
{
    public string Name { get; init; }
    public string Color { get; init; }
    
}