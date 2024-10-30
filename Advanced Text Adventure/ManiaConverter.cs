using Advanced_Text_Adventure;
using System.IO.Compression;

public class ManiaConverter
{
    public static string activeSongPath;

    public static void Convert()
    {
        List<int> timings = [];
        List<int> lanes = [];
        List<int> holds = [];
        if (!Directory.Exists(Program.dataPath))
            Directory.CreateDirectory(Program.dataPath);
        Console.WriteLine("-1: Import Song");
        string[] songPaths = Directory.GetDirectories(Program.dataPath);
        for (int i = 0; i < songPaths.Length; i++)
        {
            List<string> songData = [.. File.ReadAllText(Directory.GetFiles(songPaths[i], "*.osu")[0]).Split("\r\n")];
            int metadataIndex = songData.IndexOf("[Metadata]");
            string songName = songData[metadataIndex + 1].Split(":")[1];
            string artistName = songData[metadataIndex + 3].Split(":")[1];
            Console.WriteLine($"{i}: {songName} ({artistName})");
        }
        int songIndex = Program.InputInt(-1, songPaths.Length);
        if (songIndex > -1)
            activeSongPath = Directory.GetDirectories(Program.dataPath)[songIndex];
        else
        {
            string newPath = Console.ReadLine();
            string zipPath = newPath.Replace("\"", "");
            string folderName = zipPath.Split("\\")[^1];
            ZipFile.ExtractToDirectory(zipPath, Program.dataPath + folderName, true);
            activeSongPath = Program.dataPath + folderName;
        }
        string[] difficultyPaths = Directory.GetFiles(activeSongPath, "*.osu");
        for (int d = 0; d < difficultyPaths.Length; d++)
        {
            List<string> diffData = [.. File.ReadAllText(difficultyPaths[d]).Split("\r\n")];
            string diffName = diffData[diffData.IndexOf("[Metadata]") + 6].Split(":")[1];
            Console.WriteLine($"{d}: {diffName}");
        }
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