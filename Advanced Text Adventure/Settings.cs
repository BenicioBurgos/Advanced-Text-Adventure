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

    unsafe abstract class Setting(string name, int position)
    {
        public string name = name;
        public int position = position;
        public abstract void ChangeValue(bool increase);
        public abstract void Write(bool selected);
    } 

    unsafe class NumberSetting<T>(string name, int position, T* number, T step, T min, T max) : Setting(name, position) where T : INumber<T>
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
            Console.SetCursorPosition(0, position);
            Program.ClearLine();
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name + ": < " + *number + " >");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(name + ": " + *number + "");
            }
        }
    }

    unsafe class ColorSetting(string name, int position, ConsoleColor* color) : Setting(name, position)
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
            Console.SetCursorPosition(0, position);
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
                Console.Write(" >");
            }
            else
                Program.Write("██", 0, *color);
        }
    }

    unsafe class BoolSetting(string name, int position, bool* setting) : Setting(name, position)
    {
        public bool* setting = setting;
        
        public override void ChangeValue(bool increase)
        {
            *setting = !*setting;
        }

        public override void Write(bool selected)
        {
            Console.SetCursorPosition(0, position);
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
                Console.Write(" >");
        }
    }

    unsafe class CharArraySetting(string name, int position, char[]* chars) : Setting(name, position)
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
            Console.SetCursorPosition(0, position);
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
                    Console.Write("(Enter to rebind)");
            }
        }
    }
}
