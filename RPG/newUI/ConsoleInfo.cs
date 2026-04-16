using static System.Console;

namespace RPG.NewUI;

internal interface IWriteInfoAboutPlayer
{
    internal virtual void WriteMainStats(Player<int, long> player) {}
}

internal interface IInputInfoAboutPlayer
{
    internal virtual void InputMainStats(Player<int, long> player)
    {
        Console.WriteLine("Enter Name:");
        player.Name = Console.ReadLine();

        HP.Fill(HP.HPList);
        player.HP = HP.HPList[0];
        player.Level = 1;
        player.Experience = 0;

        Console.WriteLine("Enter the number of your class:");
        foreach(var kvp in Class.ClassesStatic)
        {
            Console.WriteLine($"{kvp.Key}. {kvp.Value}");
        }

        if (int.TryParse(Console.ReadLine(), out int index) && Class.ClassesStatic.TryGetValue(index, out string? valueClass))
        {
            player.Class = valueClass;
        }
        else
        {
            Console.WriteLine("Invalid input. Defaulting to Melee.");
            player.Class = Class.ClassesStatic.TryGetValue(1, out string? value) ? value : "Melee";
        }

        Console.WriteLine("Enter your starting location:");
        foreach (var kvp in Models.Type.ListOfTypes)
        {
            Console.WriteLine($"{kvp.Key}. {kvp.Value}");
        }
        if(int.TryParse(Console.ReadLine(), out int locIndex) && Models.Type.ListOfTypes.ContainsKey(locIndex))
        {
            player.CurrLocation = Models.Type.ListOfTypes[locIndex];
        }
        else
        {
            Console.WriteLine("Invalid input. Defaulting to Forest.");
            player.CurrLocation = Models.Type.ListOfTypes.ContainsKey(1) ? Models.Type.ListOfTypes[1] : "Forest";
        }
    }
}
internal interface IInfoAboutWorld
{
    internal virtual void WriteAllInfo(Location<int> level) { }
}