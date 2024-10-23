using System.Drawing;
using System.IO;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Advanced_Text_Adventure
{
    internal class Program
    {
        public static string[] shades = [" ", "░", "▒", "▓", "█"];

        static void Main(string[] args)
        {
            DrawImage("image");
        }

        public static void DrawImage(string name)
        {
            Bitmap image = new(Directory.GetCurrentDirectory() + "/Images/" + name + ".png");
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = image.GetPixel(x, y);
                    int value = (int)MathF.Round(MathF.Max(MathF.Max(color.R, color.G), color.B) / 255f * (shades.Length - 1));
                    float hue = color.GetHue();
                    if (MathF.Min(MathF.Min(color.R, color.G), color.B) > 200)
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = GetColor(hue);
                    Console.Write(shades[value]);
                    Console.Write(shades[value]);
                }
                Console.Write(" \r\n");
            }
        }

        public static ConsoleColor GetColor(float hue)
        { 
            if (hue >= 330 || hue <= 25) 
                return ConsoleColor.Red; 
            else if (hue >= 25 && hue <= 70)
                return ConsoleColor.Yellow;
            else if (hue >= 70 && hue <= 140)
                return ConsoleColor.Green;
            else if (hue >= 140 && hue <= 200)
                return ConsoleColor.Cyan;
            else if (hue >= 200 && hue <= 270)
                return ConsoleColor.Blue;
            else
                return ConsoleColor.Magenta;
        }
    }
}
