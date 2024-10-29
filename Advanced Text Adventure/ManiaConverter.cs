using Advanced_Text_Adventure;
using System;

public class ManiaConverter
{
    public static void Convert()
    {
        Console.SetIn(new StreamReader(Console.OpenStandardInput(131072), Console.InputEncoding, false, 131072));
        Console.WriteLine("Input note data.");
        string data = Console.ReadLine();
        Console.Clear();
        List<int> timings = [];
        List<int> lanes = [];
        List<int> holds = [];
        int phase = 0;
        string timing = "";
        string lane = "";
        string hold = "";
        for (int chara = 0; chara < data.Length; chara++)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Loading: " + MathF.Round(chara / (float)data.Length * 100) + "%");
            switch (phase)
            {
                case 0:
                    if (data[chara] != ',')
                        lane += data[chara];
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
                    break;
                case 1:
                    if (data[chara] == ',')
                        phase = 2;
                    break;
                case 2:
                    if (data[chara] != ',')
                        timing += data[chara];
                    else
                    {
                        phase = 3;
                        timings.Add(int.Parse(timing));
                    }
                    break;
                case 3: case 4:
                    if (data[chara] == ',')
                        phase++;
                    break;
                case 5:
                    if (data[chara] != ':')
                        hold += data[chara];
                    else
                    {
                        phase = 6;
                        if (hold != "0")
                            holds.Add(int.Parse(hold) - int.Parse(timing));
                        else
                            holds.Add(0);
                    }
                    break;
                case 6:
                    if (data[chara] == '|')
                    {
                        timing = "";
                        lane = "";
                        hold = "";
                        phase = 0;
                    }
                    break;
            }
        }
        Program.noteTimes = timings;
        Program.noteLanes = lanes;
        Program.noteHolds = lanes;
        Console.Clear();
    }
}