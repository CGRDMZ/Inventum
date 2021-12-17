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

        public void TransferTo(CardGroup cg) {
            if (cg == null) {
                throw new ArgumentNullException(nameof(cg));
            }
            BelongsTo = cg;
        }

        public void RemoveFromGroup() {
            BelongsTo = null;
        }

        public void ChangeColorTo(Color color)
        {
            if (color == null)
            {
                throw new ArgumentException(nameof(color));
            }

            Color = color;
        }

        internal void SwapPositionWith(Card other) {
            var temp = this.Position;
            this.ChangePosition(other.Position);
            other.ChangePosition(temp);
        }

        public void ChangePosition(int pos) {
            if (pos < 0 || pos >= int.MaxValue) {
                throw new Exception("New position cannot be smaller than 0 and bigger than max value.");
            }
            Position = pos;
        }

        public static Card CreateNew(string content, Color color, CardGroup group, int position = 0)
        {
            return new Card(content, color, group, position);
        }
    }
}