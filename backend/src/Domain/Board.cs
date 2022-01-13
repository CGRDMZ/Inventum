using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Board
    {
        public Guid BoardId { get; private set; }
        public string Name { get; private set; }
        public Color BgColor { get; private set; }

        private List<User> owners = new List<User>();
        public IReadOnlyCollection<User> Owners => owners.AsReadOnly();

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

            owners.Add(owner);

            cardGroups = new List<CardGroup>();
            activities = new List<Activity>();
        }

        public void AddNewOwner(User user)
        {
            if (user == null) throw new Exception("User can not be null.");

            var existingUsers = owners.Where(u => u.UserId == user.UserId).ToList();
            if (existingUsers.Count > 0) {
                throw new DomainException("There is already a owner with this Id");
            }

            owners.Add(user);
        }


        public void ChangeColorTo(Color color)
        {
            BgColor = color;
        }

        public void ChangeNameTo(string newName)
        {
            if (newName.Trim() == "" || newName.Trim().Length < 3)
                throw new DomainException("Board name can not be smaller than 3 characters.");

            Name = newName;
        }

        public void AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(Activity));
            activities.Add(activity);
        }

        public User OwnerWithId(Guid userId) {
            return Owners.Where(u => u.UserId == userId).Single();
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
            var canAccess = owners.Any(u => u.UserId == userId);

            return canAccess;
        }

        public static Board CreateEmptyBoard(User owner)
        {
            return new Board("New Board", Color.Constants.White, owner);
        }
    }
}
