using System.Drawing;
using System.Numerics;

namespace Advanced_Text_Adventure
{
    internal class ImageGenerator
    {
        static readonly string[] shades = [" ", "░", "▒", "▓", "█"];

        public static Pixel[] colors = [new(231, 72, 86), new(249, 241, 165), new(22, 298, 12), new(97, 214, 214), new(59, 120, 255), new(180, 0, 158), new(197, 15, 31), new(193, 156, 0), new(19, 161, 14), new(58, 150, 221), new(0, 55, 218), new(136, 23, 152)];
        public static int[] grays = [204, 118, 12, 242];
        public static ConsoleColor[] consoleColors = [ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta];
        public static ConsoleColor[] consoleGrays = [ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Black, ConsoleColor.White];
        public static string currentShade;
        public static bool light;

        public static void DrawImage(string? path, int size)
        {
            path ??= Directory.GetCurrentDirectory() + "/Images/Placeholder.png";
            Bitmap image = new(path);
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size * 2; x++)
                {
                    Color color = image.GetPixel(x * (image.Height + (image.Width - image.Height) / 2) / size / 2, y * image.Height / size);
                    Console.ForegroundColor = GetColor(color);
                    if (light) Console.BackgroundColor = ConsoleColor.White;
                    else Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(currentShade);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" \r\n");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static ConsoleColor GetColor(Color color)
        {
            float minDistance = 100000;
            int minIndex = 0;
            int shade = 0;
            if (MathF.Max(MathF.Max(color.R, color.G), color.B) - MathF.Min(MathF.Min(color.R, color.G), color.B) < 20)
            {
                for (int m = 1; m <= 4; m++)
                {
                    for (int c = 0; c < grays.Length; c++)
                    {
                        float distance = MathF.Abs(grays[c] / m - color.R);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minIndex = c;
                            shade = 5 - m;
                            light = false;
                        }
                    }
                }
                currentShade = shades[shade];
                return consoleGrays[minIndex];
            }
            else
            {
                for (int m = 1; m <= 4; m++)
                {
                    for (int c = 0; c < colors.Length; c++)
                    {
                        float distance = Vector3.Distance(Vector3.Lerp(new(colors[c].r, colors[c].g, colors[c].b), Vector3.One * 12, 1 - 0.25f * m), new(color.R, color.G, color.B));
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minIndex = c;
                            shade = m;
                            light = false;
                        }
                    }
                }
                for (int m = 1; m <= 3; m++)
                {
                    for (int c = 0; c < colors.Length; c++)
                    {
                        float distance = Vector3.Distance(Vector3.Lerp(new(colors[c].r, colors[c].g, colors[c].b), Vector3.One * 242, 1 - 0.25f * m), new(color.R, color.G, color.B));
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minIndex = c;
                            shade = m;
                            light = true;
                        }
                    }
                }
                currentShade = shades[shade];
                return consoleColors[minIndex];
            }
        }
    }

    class Pixel(int r, int g, int b)
    {
        public int r = r;
        public int g = g;
        public int b = b;
    }
}
