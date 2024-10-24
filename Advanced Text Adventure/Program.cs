using System.Drawing;
using System.IO;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Advanced_Text_Adventure
{
    internal class Program
    {
        public static readonly string[] shades = new string[] { " ", "░", "▒", "▓", "█" };
        public static readonly int[] consoleHues = new int[] { 354, 54, 116, 180, 221, 307 };

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            DrawImage("image");
            DrawImage("imageInvert");
        }

        public static void DrawImage(string name)
        {
            Bitmap image = new(Directory.GetCurrentDirectory() + "/Images/" + name + ".png");
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = image.GetPixel(x, y);
                    int saturation = (int)MathF.Round((1 - MathF.Min(MathF.Min(color.R, color.G), color.B) / 255f) * (shades.Length - 1));
                    float value = MathF.Max(MathF.Max(color.R, color.G), color.B) / 255f;
                    if (MathF.Max(MathF.Max(color.R, color.G), color.B) < 100)
                        Console.ForegroundColor = ConsoleColor.Black;
                    else
                        Console.ForegroundColor = GetColor(color.GetHue(), value < 0.75f);
                    Console.Write(shades[saturation]);
                    Console.Write(shades[saturation]);
                }
                Console.Write(" \r\n");
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static ConsoleColor GetColor(float hue, bool dark)
        {
            if (!dark)
            {
                if (hue >= 330 || hue <= 24)
                    return ConsoleColor.Red;
                else if (hue >= 24 && hue <= 85)
                    return ConsoleColor.Yellow;
                else if (hue >= 85 && hue <= 148)
                    return ConsoleColor.Green;
                else if (hue >= 148 && hue <= 200)
                    return ConsoleColor.Cyan;
                else if (hue >= 200 && hue <= 264)
                    return ConsoleColor.Blue;
                else
                    return ConsoleColor.Magenta;
            }
            else
            {
                if (hue >= 330 || hue <= 24)
                    return ConsoleColor.DarkRed;
                else if (hue >= 24 && hue <= 85)
                    return ConsoleColor.DarkYellow;
                else if (hue >= 85 && hue <= 148)
                    return ConsoleColor.DarkGreen;
                else if (hue >= 148 && hue <= 200)
                    return ConsoleColor.DarkCyan;
                else if (hue >= 200 && hue <= 264)
                    return ConsoleColor.DarkBlue;
                else
                    return ConsoleColor.DarkMagenta;
            }
        }
    }
}
