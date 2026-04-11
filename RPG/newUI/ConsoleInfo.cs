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
            WriteLine("Enter Name:");
            player.Name = ReadLine();
    
            HP.Fill(HP.HPList);
            player.HP = HP.HPList[0];
            player.Level = 1;
            player.Experience = 0;
    
            WriteLine("Enter the number of your class:");
            foreach(var kvp in Class.classes)
            {
                WriteLine($"{kvp.Key}. {kvp.Value}");
            }

            if (int.TryParse(ReadLine(), out int index) && Class.classes.ContainsKey(index))
            {
                player.Class = Class.classes[index];
            }
            else
            {
                WriteLine("Invalid input. Defaulting to Melee.");
                player.Class = Class.classes.ContainsKey(1) ? Class.classes[1] : "Melee";
            }
        }
    }
}