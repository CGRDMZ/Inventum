using System;

namespace Domain
{
    public interface ICardLocationService
    {
        void SwapCardLocations(Card first, Card second);
    }

    public class CardLocationService : ICardLocationService
    {
        public void SwapCardLocations(Card first, Card second)
        {

            if (first == null || second == null)
            {
                throw new ArgumentNullException("Cannot swap with a null card.");
            }

            if (first.BelongsTo.CardGroupId != second.BelongsTo.CardGroupId)
            {
                throw new Exception("You can not swap cards which belongs to different card groups.");
            }

            first.SwapPositionWith(second);

        }
    }
}