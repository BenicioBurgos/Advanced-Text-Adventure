using System.IO.Compression;
using System.Windows.Forms;

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

        public static void ImportSong()
        {
            Console.Title = "Importer";
            Console.WriteLine("Drag and drop an osz file into the window / paste the file path to one to import a song");
            Program.EmptyInputBuffer();
            string newPath = Console.ReadLine();
            string zipPath = newPath.Replace("\"", "");
            if (File.Exists(zipPath))
            {
                string folderName = zipPath.Split("\\")[^1];
                ZipFile.ExtractToDirectory(zipPath, Program.dataPath + folderName, true);
                Program.LoadSongs();
                Console.WriteLine("Import successful");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Invalid path");
                Thread.Sleep(1000);
            }
        }
    }
}