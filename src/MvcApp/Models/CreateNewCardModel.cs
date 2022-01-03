using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class CreateNewCardModel
    {
        [MaxLength(60)]
        public string Content { get; init; }
        
        public string BgColor { get; init; }
        
        
    }
}