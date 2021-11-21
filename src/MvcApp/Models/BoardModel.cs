using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class BoardModel
    {
        public string BoardId { get; init; }
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$")]
        public string BgColor { get; init; }
        [MinLength(5)]
        public string BoardName { get; init; }

    }
}