using System;
using System.Collections.Generic;

namespace Application.Models
{
    public class CheckListComponentDto
    {
        public Guid CheckListComponentId { get; init; }
        public string Name { get; init; }

        public List<CheckListItemDto> CheckListItems { get; init; }

    }
}