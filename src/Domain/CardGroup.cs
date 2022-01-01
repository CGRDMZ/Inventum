using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain
{
    public class CardGroup
    {
        public Guid CardGroupId { get; private set; }
        public string Name { get; private set; }
        private List<Card> cards;
        public IReadOnlyCollection<Card> Cards => cards.AsReadOnly();

        private CardGroup() { }

        public CardGroup(string name = "New Card")
        {
            CardGroupId = Guid.NewGuid();
            Name = name;

            cards = new List<Card>();
        }


        public void AddNewCard(Card newCard)
        {
            foreach (var card in cards)
            {
                if (card.CardId == newCard.CardId) throw new Exception("This card already exists in the card group.");
            }

            var nextPosition = calculateNextPosition();
            newCard.ChangePosition(nextPosition);


            cards.Add(newCard);

        }

        private int calculateNextPosition()
        {
            var lastCard = Cards.OrderBy(c => c.Position).LastOrDefault();
            return lastCard == null ? 0 : lastCard.Position + 1;
        }



        public static CardGroup CreateNewGroup(string name)
        {
            return new CardGroup(name);
        }


    }
}