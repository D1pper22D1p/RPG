using static System.Console;
using System.Runtime.CompilerServices;

namespace RPF.UI.ConsoleInfo
{
    interface IInfoAboutPlayer
    {
        internal virtual void WriteMainStats(Player<int, long> player){
            WriteLine($"Name: {player.Name}");
            WriteLine($"HP: {player.HP}");
            WriteLine($"Experience: {player.Experience}");
            WriteLine($"Class: {player.Class}");
        }
        internal virtual void InputMainStats(Player<int, long> player)
        {
            Console.WriteLine("Enter Name:");
            player.Name = ReadLine();
            Console.WriteLine("Enter HP:");
            player.HP = int.Parse(ReadLine());
            Console.WriteLine("Enter Experience:");
            player.Experience = long.Parse(ReadLine());
            Console.WriteLine("Enter Class:");
            player.Class = ReadLine();
        }
    }
}