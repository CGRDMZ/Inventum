using System.Collections.Generic;

namespace Application.Models
{
    public class BoardDto
    {
        public BoardDetailsDto BoardInfo { get; init; }
        public List<CardGroupDto> CardGroups { get; init; }
        public List<ActivityDto> Activities { get; init; }
    }
}