using System;

namespace Domain.CardComponents;

public class CheckListItem
{
    public Guid CheckListItemId { get; private set; }
    public string Content { get; private set; }
    public bool IsChecked { get; private set; }
    public bool IsDeleted { get; private set; }
    public int Position { get; private set; }
    public CheckListComponent BelongsTo { get; private set; }


    private CheckListItem() { }

    private CheckListItem(string content, CheckListComponent belongsTo, int position)
    {
        if (belongsTo == null) throw new ArgumentNullException(nameof(belongsTo));

        if (content.Trim() == "" || content.Trim().Length < 3)
            throw new DomainException("Check list item name can not be smaller than 3 characters.");

        CheckListItemId = Guid.NewGuid();
        Content = content;
        BelongsTo = belongsTo;
        Position = position;
        IsDeleted = false;
        IsChecked = false;
    }


    public void ToggleChecked()
    {
        IsChecked = !IsChecked;
    }

    public void RemoveFromList()
    {
        IsDeleted = true;
    }

    public void ChangePositionTo(int position)
    {
        if (position < 0)
        {
            throw new ArgumentException(nameof(position));
        }

        Position = position;
    }

    public static CheckListItem Create(string content, CheckListComponent belongsTo, int position)
    {
        return new CheckListItem(content, belongsTo, position);
    }


}
