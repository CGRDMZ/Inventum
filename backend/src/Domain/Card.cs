using System;
using System.Collections.Generic;
using Domain.CardComponents;

namespace Domain;
public class Card
{
    public Guid CardId { get; private set; }
    public string Content { get; private set; }
    public string Description { get; private set; }
    public Color Color { get; private set; }
    public int Position { get; private set; }
    public CardGroup BelongsTo { get; private set; }
    public bool IsDeleted { get; private set; }

    private List<CheckListComponent> checkListComponents = new List<CheckListComponent>();
    public IReadOnlyCollection<CheckListComponent> CheckListComponents => checkListComponents.AsReadOnly();

    private Card() { }

    private Card(string content, Color color, CardGroup belongsTo, int position, string description)
    {

        if (belongsTo == null) throw new DomainException("The card should be assigned to a group.");

        CardId = Guid.NewGuid();
        Content = content;
        Description = description;
        Color = color;
        BelongsTo = belongsTo;
        Position = position;
        IsDeleted = false;
    }

    public void TransferTo(CardGroup cg)
    {
        if (cg == null)
        {
            throw new ArgumentNullException(nameof(cg));
        }
        BelongsTo = cg;
    }

    public void RemoveFromGroup()
    {
        IsDeleted = true;
    }

    public void ChangeColorTo(Color color)
    {
        if (color == null)
        {
            throw new ArgumentException(nameof(color));
        }

        Color = color;
    }

    internal void SwapPositionWith(Card other)
    {
        var temp = this.Position;
        this.ChangePosition(other.Position);
        other.ChangePosition(temp);
    }

    public void ChangePosition(int pos)
    {
        if (pos < 0 || pos >= int.MaxValue)
        {
            throw new Exception("New position cannot be smaller than 0 and bigger than max value.");
        }
        Position = pos;
    }

    public void UpdateDescription(string description)
    {
        if (description == null)
        {
            throw new ArgumentException(nameof(description));
        }

        if (description.Trim().Length > 255)
        {
            throw new DomainException("Description cannot be longer than 255 characters.");
        }

        Description = description.Trim();
    }

    public void AddNewCheckListComponent(CheckListComponent checkListComponent)
    {
        if (checkListComponent is null)
        {
            throw new ArgumentNullException(nameof(checkListComponent));
        }

        checkListComponents.Add(checkListComponent);
    }

    public static Card CreateNew(string content, Color color, CardGroup group, int position = 0)
    {
        return new Card(content, color, group, position, "");
    }
}
