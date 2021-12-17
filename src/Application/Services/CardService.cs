using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;

namespace Application.Services
{
    public class CardService : ICardService
    {

        public CardService()
        {
        }

        public IEnumerable<Card> GetCards(Board board, Guid userId, Guid cardGroupId, IEnumerable<Guid> cardIds)
        {

            if (!board.IsAccessiableBy(userId))
            {
                throw new UserNotAuthorizedException();
            }

            var cardGroup = board.CardGroups.SingleOrDefault(cg => cg.CardGroupId == cardGroupId);
            if (cardGroup == null)
            {
                throw new ResourceNotFoundException($"There is no existing card group with this id: {cardGroupId}");
            }

            var cards = new List<Card>();

            foreach (var cardId in cardIds)
            {
                var card = cardGroup.Cards.SingleOrDefault(c => c.CardId == cardId);
                if (card == null)
                {
                    throw new ResourceNotFoundException($"Card with id: {cardId.ToString()} does not exist.");
                }
                cards.Add(card);
            }

            return cards;

        }
    }

}