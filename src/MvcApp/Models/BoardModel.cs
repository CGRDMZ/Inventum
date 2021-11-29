using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class BoardMetadataModel
    {
        public string BoardId { get; init; }

        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$")]
        public string BgColor { get; init; }
        
        [MinLength(3)]
        public string BoardName { get; init; }
    }
}