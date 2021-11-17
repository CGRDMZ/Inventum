using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class Board
    {


        public Guid BoardId { get; private set; }
        public string Name { get; private set; }
        public Color BgColor {get; private set; }
        public User Owner { get; private set; }

        private List<CardGroup> cardGroups = new List<CardGroup>();
        public ReadOnlyCollection<CardGroup> CardGroups => cardGroups.AsReadOnly();

        private Board() {}

        public Board(string name, Color bgColor, User owner) {
            BoardId = Guid.NewGuid();
            Name = name;
            BgColor = bgColor;
            Owner = owner;

            cardGroups = new List<CardGroup>();
        }

        public void AssignTo(User user) {
            Owner = user;
        }

        public void ChangeColorTo(Color color) {
            BgColor = color;
        }

        public static Board CreateEmptyBoard(User owner) {
            return new Board("New Board", Color.Constants.White, owner);
        }
    }
}
