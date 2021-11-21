using System;

namespace Domain
{
    public class Card
    {
        public Guid CardId { get; private set; }
        public string Content { get; private set; }
        public Color Color { get; private set; }
        public int Position { get; private set; }
        public CardGroup BelongsTo { get; private set; }

        private Card() { }

        public Card(string content, Color color, CardGroup belongsTo, int position)
        {

            if (belongsTo == null) throw new Exception("The card should be assigned to a group.");

            CardId = Guid.NewGuid();
            Content = content;
            Color = color;
            BelongsTo = belongsTo;
            Position = position;
        }

        public void ChangeColorTo(Color color)
        {
            if (color == null)
            {
                throw new ArgumentException(nameof(color));
            }

            Color = color;
        }

        public static Card CreateNew(CardGroup group, int position)
        {
            return new Card("This is where you write the content of this card.", Color.Constants.White, group, position);
        }
    }
}