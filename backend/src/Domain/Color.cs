using System.Globalization;

namespace Domain
{
    public class Color
    {
        public int Red { get; init; }
        public int Green { get; init; }
        public int Blue { get; init; }

        public override bool Equals(object obj)
        {
            Color other = (Color)obj;
            if (other == null) return false;
            return Red == other.Red && Green == other.Green && Blue == other.Blue;
        }

        public override string ToString()
        {
            return $"#{Red.ToString("X2")}{Green.ToString("X2")}{Blue.ToString("X2")}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + Red.GetHashCode();
                hash = hash * 23 + Green.GetHashCode();
                hash = hash * 23 + Blue.GetHashCode();
                return hash;
            }
        }

        public static Color FromHexCode(string hex)
        {
            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }
            return new Color()
            {
                Red = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                Green = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                Blue = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber),
            };
        }

        public static class Constants
        {
            public static Color White => new Color() { Red = 255, Green = 255, Blue = 255 };
            public static Color Red => new Color() { Red = 255, Green = 0, Blue = 0 };
            public static Color Green => new Color() { Red = 0, Green = 255, Blue = 0 };
            public static Color Blue => new Color() { Red = 0, Green = 0, Blue = 255 };

        }


        // Green,
        // Blue,
        // Red
    }


}