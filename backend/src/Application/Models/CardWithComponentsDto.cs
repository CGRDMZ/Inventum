using System.Collections.Generic;

namespace Application.Models
{
    public class CardWithComponentsDto
    {
        public string CardId { get; init; }
        public string Content { get; init; }
        public string CardColor { get; init; }
        public int Position { get; init; }

        public List<CheckListComponentDto> CheckListComponents { get; init; }


    }
}