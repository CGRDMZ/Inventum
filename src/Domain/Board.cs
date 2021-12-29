using System;
using System.Collections.Generic;

namespace Domain
{
    public class Board
    {
        public Guid BoardId { get; private set; }
        public string Name { get; private set; }
        public Color BgColor { get; private set; }
        public User Owner { get; private set; }

        private List<CardGroup> cardGroups = new List<CardGroup>();
        public IReadOnlyCollection<CardGroup> CardGroups => cardGroups.AsReadOnly();


        private List<Activity> activities = new List<Activity>();
        public IReadOnlyCollection<Activity> Activities => activities.AsReadOnly();

        private Board() { }

        public Board(string name, Color bgColor, User owner)
        {
            BoardId = Guid.NewGuid();
            Name = name;
            BgColor = bgColor;
            Owner = owner;

            cardGroups = new List<CardGroup>();
            activities = new List<Activity>();
        }

        public void AssignTo(User user)
        {
            Owner = user;
        }

        public void ChangeColorTo(Color color)
        {
            BgColor = color;
        }

        public void ChangeNameTo(string newName)
        {
            if (newName.Trim() == "" || newName.Trim().Length < 3)
                throw new ArgumentException("Board name can not be smaller than 3 characters.");

            Name = newName;
        }

        public void AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(Activity));
            activities.Add(activity);
        }

        public void AddNewCardGroup(string name)
        {
            if (name == null)
            {
                name = "New Card Group";
            }

            CardGroup newGroup = CardGroup.CreateNewGroup(name);
            cardGroups.Add(newGroup);
        }

        public bool IsAccessiableBy(Guid userId)
        {
            return Owner.UserId == userId;
        }

        public static Board CreateEmptyBoard(User owner)
        {
            return new Board("New Board", Color.Constants.White, owner);
        }
    }
}
