using System;

namespace Domain
{
    public class Card
    {
        public Guid CardId { get; private set; }
        public string Content { get; private set; }
        public Color Color { get; private set; }
        public CardGroup BelongsTo { get; private set; }

        private Card() { }

        public Card(string content, Color color, CardGroup belongsTo)
        {
            CardId = Guid.NewGuid();
            Content = content;
            Color = color;
            BelongsTo = belongsTo;
        }

        public void ChangeColorTo(Color color)
        {
            if (color == null)
            {
                throw new ArgumentException(nameof(color));
            }

            Color = color;
        }

        public static Card CreateNew(CardGroup group)
        {
            return new Card("This is where you write the content of this card.", Color.Constants.White, group);
        }
    }
}