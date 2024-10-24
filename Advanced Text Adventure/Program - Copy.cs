using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Advanced_Text_Adventure
{
    internal class ProgramOther
    {
//        public static readonly string[] shades = new string[] { " ", "░", "▒", "▓", "█" };
//        public static readonly int[] consoleHues = new int[] { 354, 54, 116, 180, 221, 307 };
//
//        static void Main(string[] args)
//        {
//            Console.BackgroundColor = ConsoleColor.White;
//            DrawImage("image");
//            DrawImage("imageInvert");
//            DrawImage("cayden");
//        }
//
//        public static void DrawImage(string name)
//        {
//            Bitmap image = new(Directory.GetCurrentDirectory() + "/Images/" + name + ".png");
//            for (int y = 0; y < image.Height; y++)
//            {
//                for (int x = 0; x < image.Width; x++)
//                {
//                    Color color = image.GetPixel(x, y);
//                    float value = MathF.Max(MathF.Max(color.R, color.G), color.B) / 255f;
//                    float saturation = 0;
//                    if (value != 0)
//                        saturation = (value - MathF.Min(MathF.Min(color.R, color.G), color.B) / 255f) / value;
//                    //Console.Write(saturation);
//                    int saturationValue = (int)MathF.Round((1 - saturation) * (shades.Length - 1));
//                    if (saturation < 0.25f)
//                    {
//                        if (value < 0.25f)
//                            Console.ForegroundColor = ConsoleColor.Black;
//                        else if (value < 0.5f)
//                            Console.ForegroundColor = ConsoleColor.DarkGray;
//                        else if (value < 0.75f)
//                            Console.ForegroundColor = ConsoleColor.Gray;
//                    }
//                    else
//                        Console.ForegroundColor = GetColor(color.GetHue(), value < 0.75f);
//                    Console.Write(shades[saturationValue]);
//                    Console.Write(shades[saturationValue]);
//                }
//                Console.Write(" \r\n");
//            }
//            Console.ForegroundColor = ConsoleColor.Black;
//        }
//
//        public static ConsoleColor GetColor(float hue, bool dark)
//        {
//            if (!dark)
//            {
//                if (hue >= 330 || hue <= 24)
//                    return ConsoleColor.Red;
//                else if (hue >= 24 && hue <= 85)
//                    return ConsoleColor.Yellow;
//                else if (hue >= 85 && hue <= 148)
//                    return ConsoleColor.Green;
//                else if (hue >= 148 && hue <= 200)
//                    return ConsoleColor.Cyan;
//                else if (hue >= 200 && hue <= 264)
//                    return ConsoleColor.Blue;
//                else
//                    return ConsoleColor.Magenta;
//            }
//            else
//            {
//                if (hue >= 330 || hue <= 24)
//                    return ConsoleColor.DarkRed;
//                else if (hue >= 24 && hue <= 85)
//                    return ConsoleColor.DarkYellow;
//                else if (hue >= 85 && hue <= 148)
//                    return ConsoleColor.DarkGreen;
//                else if (hue >= 148 && hue <= 200)
//                    return ConsoleColor.DarkCyan;
//                else if (hue >= 200 && hue <= 264)
//                    return ConsoleColor.DarkBlue;
//                else
//                    return ConsoleColor.DarkMagenta;
//            }
//        }
        //public static ConsoleColor FindColor(float hue, int darknessLevel, Vector2 pos)
        //{
        //    switch (darknessLevel)
        //    {
        //        case 0:
        //            {
        //                return ConsoleColor.Black;
        //            }
        //        case 1:
        //            {
        //                if ((pos.X + pos.Y) % 2 == 0)
        //                    return ConsoleColor.Black;
        //                else
        //                    return GetColor(true, hue);
        //            }
        //        case 2:
        //            {
        //                return GetColor(true, hue);
        //            }
        //        case 3:
        //            {
        //                if ((pos.X + pos.Y) % 2 == 0)
        //                    return GetColor(true, hue);
        //                else
        //                    return GetColor(false, hue);
        //            }
        //        case 4:
        //            {
        //                return GetColor(false, hue);
        //            }
        //    }
        //    return ConsoleColor.White;
        //}
        //
        //public static ConsoleColor GetColor(bool dark, float hue)
        //{
        //    if (dark)
        //    {
        //        if (hue >= 330 || hue <= 24)
        //            return ConsoleColor.DarkRed;
        //        else if (hue >= 24 && hue <= 85)
        //            return ConsoleColor.DarkYellow;
        //        else if (hue >= 85 && hue <= 148)
        //            return ConsoleColor.DarkGreen;
        //        else if (hue >= 148 && hue <= 200)
        //            return ConsoleColor.DarkCyan;
        //        else if (hue >= 200 && hue <= 264)
        //            return ConsoleColor.DarkBlue;
        //        else
        //            return ConsoleColor.DarkMagenta;
        //    }
        //    else
        //    {
        //        if (hue >= 330 || hue <= 24)
        //            return ConsoleColor.Red;
        //        else if (hue >= 24 && hue <= 85)
        //            return ConsoleColor.Yellow;
        //        else if (hue >= 85 && hue <= 148)
        //            return ConsoleColor.Green;
        //        else if (hue >= 148 && hue <= 200)
        //            return ConsoleColor.Cyan;
        //        else if (hue >= 200 && hue <= 264)
        //            return ConsoleColor.Blue;
        //        else
        //            return ConsoleColor.Magenta;
        //    }
        //}
    }
}
