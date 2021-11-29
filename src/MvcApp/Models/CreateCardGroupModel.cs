using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class CreateCardGroupModel
    {
        [MinLength(3)]
        public string CardGroupName { get; init; }
    }
}