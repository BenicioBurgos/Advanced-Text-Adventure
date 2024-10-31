using Advanced_Text_Adventure;
using System.IO.Compression;

public class ManiaConverter
{
    public static void ReadData()
    {
        List<int> timings = [];
        List<int> lanes = [];
        List<int> holds = [];
        Console.Clear();
        Console.WriteLine("Loading...");
        List<string> dataList = [.. File.ReadAllText(Program.songs[Program.selectedSong].chartPaths[Program.selectedDifficulty]).Split("\r\n")];
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

    public static void ImportSong()
    {
        Console.WriteLine("Drag and drop an osz file into the window to import a song");
        string newPath = Console.ReadLine();
        string zipPath = newPath.Replace("\"", "");
        if (File.Exists(zipPath))
        {
            string folderName = zipPath.Split("\\")[^1];
            ZipFile.ExtractToDirectory(zipPath, Program.dataPath + folderName, true);
            Program.LoadSongs();
            Console.WriteLine("Import successful");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Invalid path");
            Console.ReadLine();
        }
    }
}