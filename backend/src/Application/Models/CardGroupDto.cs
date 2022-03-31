using System.Collections.Generic;

namespace Application.Models
{
    public class CardGroupDto
    {
        public string CardGroupId { get; init; }

        public string Name { get; init; }

        public List<CardDto> Cards { get; init; }


    }
}