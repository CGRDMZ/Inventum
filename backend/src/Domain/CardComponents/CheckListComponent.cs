using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain.CardComponents
{
    public class CheckListComponent
    {
        public Guid CheckListComponentId { get; private set; }
        public string Name { get; private set; }

        public bool IsDeleted { get; private set; }

        public Card BelongsTo { get; private set; }

        private List<CheckListItem> checkListItems = new List<CheckListItem>();
        public IReadOnlyCollection<CheckListItem> CheckListItems => checkListItems.AsReadOnly();

        private CheckListComponent() { }

        public CheckListComponent(string name)
        {
            CheckListComponentId = Guid.NewGuid();
            Name = name;
        }


        public void AddNewItem(string name)
        {
            if (name == null) throw new Exception("Name can not be null.");

            var position = calculateNextPosition();

            checkListItems.Add(CheckListItem.Create(name, this, position));
        }

        private int calculateNextPosition()
        {
            var lastCard = checkListItems.OrderBy(c => c.Position).LastOrDefault();
            return lastCard == null ? 0 : lastCard.Position + 1;
        }

        public void MoveItem(CheckListItem item, int newPosition)
        {
            if (item == null) throw new Exception("Item can not be null.");

            var orderedList = checkListItems.OrderByDescending(i => i.Position).ToList();

            orderedList.Insert(newPosition, item);

            int idx = 0;
            orderedList.ForEach(i => i.ChangePositionTo(idx++));
        }

        public void RemoveComponent()
        {
            IsDeleted = true;
        }

        public static CheckListComponent Create(string name)
        {
            return new CheckListComponent(name);
        }


    }
}