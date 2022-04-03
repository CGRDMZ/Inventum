using System;

namespace Application.Models
{
    public class CheckListItemDto
    {
        public Guid CheckListItemId { get; init; }
        public string Content { get; init; }
        public bool IsChecked { get; init; }
        public int Position { get; init; }
    }
}