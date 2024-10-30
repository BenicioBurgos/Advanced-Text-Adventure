using Advanced_Text_Adventure;
using System.IO;
using System.IO.Compression;

public class ManiaConverter
{
    public static string activeSongPath;

    public static void Convert()
    {
        List<int> timings = [];
        List<int> lanes = [];
        List<int> holds = [];
        string zipPath = Console.ReadLine().Replace("\"", "");
        string folderName = zipPath.Split("\\")[^1];
        ZipFile.ExtractToDirectory(zipPath, Directory.GetCurrentDirectory() + "/Data/" + folderName, true);
        activeSongPath = Directory.GetCurrentDirectory() + "/Data/" + folderName;
        int diff = int.Parse(Console.ReadLine());
        Console.WriteLine("Loading...");
        List<string> dataList = [.. File.ReadAllText(Directory.GetFiles(activeSongPath, "*.osu")[diff]).Split("\r\n")];
        int l = dataList.IndexOf("[HitObjects]");
        for (int i = 0; i <= l; i++)
            dataList.RemoveAt(0);
        foreach (string line in dataList)
        {
            int phase = 0;
            string timing = "";
            string lane = "";
            string hold = "";
            for (int chara = 0; chara < line.Length; chara++)
            {
                Console.SetCursorPosition(0, 0);
                switch (phase)
                {
                    case 0:
                        if (line[chara] != ',')
                            lane += line[chara];
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
                        if (line[chara] == ',')
                            phase = 2;
                        break;
                    case 2:
                        if (line[chara] != ',')
                            timing += line[chara];
                        else
                        {
                            phase = 3;
                            timings.Add(int.Parse(timing));
                        }
                        break;
                    case 3: case 4:
                        if (line[chara] == ',')
                            phase++;
                        break;
                    case 5:
                        if (line[chara] != ':')
                            hold += line[chara];
                        else
                        {
                            if (hold != "0")
                                holds.Add(int.Parse(hold) - int.Parse(timing));
                            else
                                holds.Add(0);
                            goto end;
                        }
                        break;
                }
            }
        end:;
        }
        Program.noteTimes = timings;
        Program.noteLanes = lanes;
        Program.noteHolds = holds;
        Console.Clear();
    }
}