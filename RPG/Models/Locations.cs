using System.Data.Common;
using System.Dynamic;

namespace RPG.Models;

internal abstract class Location<I>
{
    internal I? Id { get; set; }
    internal string Name
    {
        get => FirstUpSymbolToUpper.Apply(field);
        set;
    } = "";
    internal I? Difficulty { get; set; }
    internal string? Type { get; set;}
}

internal class LocationsInformation : Location<int>, IInfoAboutWorld
{
    internal static void WriteAllInfo(Location<int> level)
    {
        Console.WriteLine("Level Information:");
        Console.WriteLine(level.Id);
        Console.WriteLine(level.Name);
        Console.WriteLine(level.Difficulty);
        Console.WriteLine(level.Type);
    }
}

internal class Type
{
    internal Dictionary<int, string> Types = ListOfTypes;
    internal static readonly Dictionary<int, string> ListOfTypes = new()
    {
        {1, "Forest"},
        {2, "Ruins"},
        {3, "Spider Cave"}
    };
}