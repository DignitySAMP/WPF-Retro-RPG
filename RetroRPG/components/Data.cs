using System;
using System.Windows.Media;

namespace RetroRPG.components
{
    public enum RetroColor
    {
        Black = 0,
        Blue = 1,
        Green = 2,
        Cyan = 3,
        Red = 4,
        Magenta = 5,
        Brown = 20,
        LightGray = 7,
        DarkGray = 56,
        BrightBlue = 57,
        BrightGreen = 58,
        BrightCyan = 59,
        BrightRed = 60,
        BrightMagenta = 61,
        BrightYellow = 62,
        BrightWhite = 63
    }

    public static class RetroColorConverter
    {
        public static Brush ToBrush(RetroColor retroColor)
        {
            Color color = ToColor(retroColor);
            return new SolidColorBrush(color);
        }

        public static Color ToColor(RetroColor retroColor)
        {
            switch (retroColor)
            {
                case RetroColor.Black: return Colors.Black;
                case RetroColor.Blue: return Colors.Blue;
                case RetroColor.Green: return Colors.Green;
                case RetroColor.Cyan: return Color.FromRgb(0, 170, 170);
                case RetroColor.Red: return Colors.Red;
                case RetroColor.Magenta: return Colors.Magenta;
                case RetroColor.Brown: return Color.FromRgb(170, 85, 0);
                case RetroColor.LightGray: return Colors.LightGray;
                case RetroColor.DarkGray: return Color.FromRgb(85, 85, 85);
                case RetroColor.BrightBlue: return Color.FromRgb(85, 85, 255);
                case RetroColor.BrightGreen: return Color.FromRgb(85, 255, 85);
                case RetroColor.BrightCyan: return Color.FromRgb(85, 255, 255);
                case RetroColor.BrightRed: return Color.FromRgb(255, 85, 85);
                case RetroColor.BrightMagenta: return Color.FromRgb(255, 85, 255);
                case RetroColor.BrightYellow: return Color.FromRgb(255, 255, 85);
                case RetroColor.BrightWhite: return Colors.White;
                default: throw new ArgumentException("Invalid RetroColor value.");
            }
        }
    }
}
