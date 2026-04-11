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
            WriteLine($"Current location: {player.currLocation}");
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
            
            WriteLine("Enter your starting location:");
            foreach (var kvp in Models.Levels.Type.types)
            {
                WriteLine($"{kvp.Key}. {kvp.Value}");
            }
            if(int.TryParse(ReadLine(), out int locIndex) && Models.Levels.Type.types.ContainsKey(locIndex))
            {
                player.currLocation = Models.Levels.Type.types[locIndex];
            }
            else
            {
                WriteLine("Invalid input. Defaulting to Forest.");
                player.currLocation = Models.Levels.Type.types.ContainsKey(1) ? Models.Levels.Type.types[1] : "Forest";
            }
        }
    }
    internal interface IInfoAboutWorld
    {
        internal virtual void WriteAllInfo(Level<int> level)
        {
            WriteLine("Level Information:");
            WriteLine(level.id);
            WriteLine(level.name);
            WriteLine(level.difficulty);
            WriteLine(level.type);
        }
    }
}