using System.Drawing;
using System.IO;

namespace Advanced_Text_Adventure
{
    internal class Program
    {
        public static string[] shades = [" ", "░", "▒", "▓", "█"];

        static void Main(string[] args)
        {
            for (int troll = 0; troll < 100; troll++)
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
                    int lightLevel = (int)MathF.Round(MathF.Max(MathF.Max(color.R, color.G), color.B) / 255f * (shades.Length - 1));
                    Console.Write(shades[lightLevel]);
                    Console.Write(shades[lightLevel]);
                }
                Console.Write(" \r\n");
            }
        }
    }
}
