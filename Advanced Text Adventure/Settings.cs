using System.Numerics;

#pragma warning disable 8500
namespace Advanced_Text_Adventure
{
    unsafe internal class Settings
    {
        public static List<Setting> settings = [];
        public static int settingSelected;
        public static bool changingSetting;

        public static void DisplaySettings()
        {
            Console.Title = "Settings";
            settingSelected = 0;
            WriteSettings();
            while (true)
            {
                if (Program.menuInputsDown[0])
                {
                    settingSelected = (int)MathF.Max(settingSelected - 1, 0);
                    WriteSettings();
                }
                else if (Program.menuInputsDown[1])
                {
                    settingSelected = (int)MathF.Min(settingSelected + 1, settings.Count - 1);
                    WriteSettings();
                }
                if (settings[settingSelected].GetType() == typeof(CharArraySetting) && Program.menuInputsDown[2])
                {
                    settings[settingSelected].ChangeValue(false);
                    WriteSettings();
                }
                else
                {
                    if (Program.menuInputsDown[4])
                    {
                        settings[settingSelected].ChangeValue(false);
                        WriteSettings();
                    }
                    else if (Program.menuInputsDown[5])
                    {
                        settings[settingSelected].ChangeValue(true);
                        WriteSettings();
                    } 
                }
                if (Program.menuInputsDown[3])
                    break;
            }
        }

        public static void WriteSettings()
        {
            Console.SetCursorPosition(0, 0);
            for (int s = 0; s < settings.Count; s++)
                settings[s].Write(s == settingSelected);
        }

        public static char WaitForInput()
        {
            Program.EmptyInputBuffer();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    char input = char.ToLower(Console.ReadKey(true).KeyChar);
                    if ((!char.IsWhiteSpace(input) && (char.IsAsciiLetterOrDigit(input) || char.IsPunctuation(input))) || input == ' ')
                        return input;
                }
            }
        }
    }

    unsafe abstract class Setting(string name)
    {
        public string name = name;
        public abstract void ChangeValue(bool increase);
        public abstract void Write(bool selected);
    } 

    unsafe class NumberSetting<T>(string name, T* number, T step, T min, T max) : Setting(name) where T : INumber<T>
    {
        public T* number = number;
        public T step = step;
        public T min = min;
        public T max = max;

        public override void ChangeValue(bool increase)
        {
            if (increase && *number + step <= max)
                *number += step;
            else if (!increase && *number - step >= min)
                *number -= step;
        }

        public override void Write(bool selected)
        {
            Program.ClearLine();
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name + ": < " + *number + " >\r\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(name + ": " + *number + "\r\n");
            }
        }
    }

    unsafe class ColorSetting(string name, ConsoleColor* color) : Setting(name)
    {
        public ConsoleColor* color = color;
        readonly List<ConsoleColor> colors = [ConsoleColor.White, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Magenta, ConsoleColor.DarkMagenta];
        
        public override void ChangeValue(bool increase)
        {
            int index = colors.IndexOf(*color);
            if (increase && index < colors.Count - 1)
                *color = colors[index + 1];
            else if (!increase && index > 0)
                *color = colors[index - 1];
        }

        public override void Write(bool selected)
        {
            Program.ClearLine();
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name + ": < ");
            }
            else 
            { 
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(name + ": ");
            }
            if (selected)
            {
                Program.Write("██", 0, *color);
                Console.Write(" >\r\n");
            }
            else
                Program.Write("██\r\n", 0, *color);
        }
    }

    unsafe class BoolSetting(string name, bool* setting) : Setting(name)
    {
        public bool* setting = setting;
        
        public override void ChangeValue(bool increase)
        {
            *setting = !*setting;
        }

        public override void Write(bool selected)
        {
            Program.ClearLine();
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name + ": < ");
            }
            else 
            { 
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(name + ": ");
            }
            if (*setting)
                Console.Write("On");
            else
                Console.Write("Off");
            if (selected)
                Console.Write(" >\r\n");
            else
                Console.Write("\r\n");
        }
    }

    unsafe class CharArraySetting(string name, char[]* chars) : Setting(name)
    {
        public char[]* chars = chars;
    
        public override void ChangeValue(bool increase = false)
        {
            Settings.changingSetting = true;
            for (int c = 0; c < (*chars).Length; c++)
            {
                (*chars)[c] = '_';
                Settings.WriteSettings();
                (*chars)[c] = Settings.WaitForInput();
            }
            Settings.changingSetting = false;
            Settings.WriteSettings();
        }
    
        public override void Write(bool selected)
        {
            Program.ClearLine();
            if (selected)
                Console.ForegroundColor = ConsoleColor.White;
            else
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(name + ":");
            for (int c = 0; c < (*chars).Length; c++)
            {
                Console.Write(" " + (*chars)[c] + " ");
                if (c != (*chars).Length - 1)
                    Console.Write("|");
                else if (selected && !Settings.changingSetting)
                    Console.Write("(Enter to rebind)\r\n");
                else
                    Console.Write("\r\n");
            }
        }
    }
}
