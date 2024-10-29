using Advanced_Text_Adventure;
using System;

public class ManiaConverter
{
    public static void Convert()
    {
        Console.SetIn(new StreamReader(Console.OpenStandardInput(65536), Console.InputEncoding, false, 65536));
        Console.WriteLine("Input note data.");
        string data = Console.ReadLine();
        Console.Clear();
        List<int> timings = [];
        List<int> lanes = [];
        string holds = "Hold list: ";
        int phase = 0;
        string timing = "";
        string lane = "";
        string hold = "";
        for (int chara = 0; chara < data.Length; chara++)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(MathF.Round(chara / (float)data.Length * 10000) / 100);
            if (phase == 0)
            {
                if (data[chara] != ',')
                {
                    lane += data[chara];
                }
                else
                {
                    phase = 1;
                    if (lane == "64")
                        lane = "0";
                    else if (lane == "192")
                        lane = "1";
                    else if (lane == "320")
                        lane = "2";
                    else if (lane == "448")
                        lane = "3";
                    lanes.Add(int.Parse(lane));
                }
            }
            else if (phase == 1)
            {
                if (data[chara] == ',')
                {
                    phase = 2;
                }
            }
            else if (phase == 2)
            {
                if (data[chara] != ',')
                {
                    timing += data[chara];
                }
                else
                {
                    phase = 3;
                    timings.Add(int.Parse(timing));
                }
            }
            else if (phase == 3 || phase == 4)
            {
                if (data[chara] == ',')
                {
                    phase++;
                }
            }
            else if (phase == 5)
            {
                if (data[chara] != ':')
                {
                    hold += data[chara];
                }
                else
                {
                    phase = 6;
                    if (hold != "0")
                    {
                        holds += int.Parse(hold) - int.Parse(timing) + ",";
                    }
                    else
                    {
                        holds += "0,";
                    }
                }
            }
            else if (phase == 6)
            {
                if (data[chara] == '/')
                {
                    timing = "";
                    lane = "";
                    hold = "";
                    phase = 0;
                }
            }
        }
        //Console.WriteLine(timings);
        //Console.WriteLine(lanes);
        //Console.WriteLine(holds);
        Program.noteTimes = timings;
        Program.noteLanes = lanes;
        Console.Clear();
    }
}