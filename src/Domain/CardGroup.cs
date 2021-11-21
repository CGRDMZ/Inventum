using System;
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
                if (card.CardId == newCard.CardId) throw new Exception("This car already exists in the card group.");
            }


            cards.Add(newCard);

        }


        public static CardGroup CreateNewGroup(string name)
        {
            return new CardGroup(name);
        }


    }
}