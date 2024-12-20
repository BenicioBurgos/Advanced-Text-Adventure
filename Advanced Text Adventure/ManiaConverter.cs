﻿using System.IO.Compression;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Advanced_Text_Adventure
{
    internal class ManiaConverter
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
            Program.currentKeys = Program.songs[Program.selectedSong].keyCounts[Program.selectedDifficulty];
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
                                phase++;
                                lanes.Add((int)MathF.Floor(int.Parse(lane) * Program.currentKeys / 512f));
                            }
                            break;
                        case 1:
                            if (line[chara] == ',')
                                phase++;
                            break;
                        case 2:
                            if (line[chara] != ',')
                                timing += line[chara];
                            else
                            {
                                phase++;
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
                                if (hold != "0" && int.Parse(hold) > timings[^1])
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

        public static void ImportSong(bool delete)
        {
            Console.Title = "Importer";
            Console.WriteLine("Drag and drop an osz file into the window / paste the file path to one to import a song");
            Program.EmptyInputBuffer();
            string newPath = "" + Console.ReadLine();
            string zipPath = newPath.Replace("\"", "");
            string[] paths = [];
            if (File.Exists(zipPath) && zipPath.Split(".")[^1] == "osz")
                paths = [zipPath];
            else if (Directory.Exists(zipPath))
                paths = Directory.GetDirectories(zipPath + "\\");
            if (paths.Length > 0)
            {
                foreach (string path in paths)
                {
                    Console.WriteLine(path);
                    Console.WriteLine("Importing...");
                    string folderName = path.Split("\\")[^1];
                    if (folderName[folderName.Length - 4] == '.')
                    {
                        folderName = folderName.Remove(folderName.Length - 4, 4);
                        ZipFile.ExtractToDirectory(path, Program.dataPath + folderName, true);
                    }
                    else
                        CopyDirectory(path, Directory.CreateDirectory(Program.dataPath + folderName).FullName);
                    List<string> songPaths = [];
                    string[] difficultyPaths = Directory.GetFiles(Program.dataPath + folderName, "*.osu");
                    for (int d = 0; d < difficultyPaths.Length; d++)
                    {
                        List<string> diffData = [.. File.ReadAllText(difficultyPaths[d]).Split("\r\n")];
                        songPaths.Add(Program.dataPath + folderName + "\\" + diffData[diffData.IndexOf("[General]") + 1].Split(": ")[1]);
                    }
                    List<string> audioFiles = [.. Directory.GetFiles(Program.dataPath + folderName, "*.wav"), .. Directory.GetFiles(Program.dataPath + folderName, "*.ogg"), .. Directory.GetFiles(Program.dataPath + folderName, "*.mp3")];
                    foreach (string file in audioFiles)
                        if (!songPaths.Contains(file))
                            File.Delete(file);
                    if (delete)
                        File.Delete(path);
                }
                Program.LoadSongs();
                Console.WriteLine("Import successful");
            }
            else
                Console.WriteLine("Invalid path");
            Thread.Sleep(1000);
        }

        static void CopyDirectory(string sourcePath, string targetPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}