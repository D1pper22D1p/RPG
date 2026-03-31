using static System.Console;

namespace RPG.newUI.ConsoleInfo
{
    internal interface IInfoAboutPlayer
    {
        internal virtual void WriteMainStats(Player<int, long> player){
            WriteLine($"Name: {player.Name}");
            WriteLine($"HP: {player.HP}");
            WriteLine($"Level: {player.Level}");
            WriteLine($"Experience: {player.Experience}");
            WriteLine($"Class: {player.Class}");
        }
        internal virtual void InputMainStats(Player<int, long> player)
        {
            if (!File.Exists(fileManage.directoryPath))
            {
                WriteLine("Enter Name:");
                player.Name = ReadLine();
                HP.Fill(HP.HPList);
                player.HP = HP.HPList[0];
                player.Level = 1;
                player.Experience = 0;
                WriteLine("Enter the number of ur class:");
                if (int.TryParse(ReadLine(), out int index) && index >= 0 && index < Class.Classes.Count)
                    player.Class = Class.Classes.ElementAt(index).Value;
                else
                    WriteLine("Invalid input. Please enter a valid number.");
            }else
            {
                string[] lines = File.ReadAllLines(fileManage.directoryPath);
                if (lines.Length == 5)
                {
                    player.Name = lines[0];
                    player.HP = int.Parse(lines[1]);
                    player.Level = int.Parse(lines[2]);
                    player.Experience = long.Parse(lines[3]);
                    player.Class = Class.Classes.ElementAt(int.Parse(lines[4])).Value;
                }
            }
        }
    }
}