using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;

namespace RPG.Models;

internal class Player<I, L> : IInputInfoAboutPlayer
{
    private byte _dataVersion = 1;
    [JsonProperty("name")]
    internal string Name
    {
        get => FirstUpSymbolToUpper.Apply(field);
        set;
    } = "";
    [JsonProperty("hp")]
    internal I? HP { get; set; }
    [JsonProperty("level")]
    internal I? Level { get; set; }
    [JsonProperty("experience")]
    internal L? Experience { get; set; }
    [JsonProperty("class")]
    internal string Class { get; set; } = "";
    [JsonProperty("location")]
    internal string CurrLocation { get; set; } = "";
    public Player(){}

    [OnDeserialized]
    internal void OnDeserialized(StreamingContext context)
    {
        if (string.IsNullOrEmpty(CurrLocation))
            CurrLocation = "Forest";
    }

    internal void MigrateData()
    {
        if(_dataVersion < 1)
        {
            CurrLocation = "Forest";
            _dataVersion += 1;
        }
    }
}

internal class PlayerInfo : IWriteInfoAboutPlayer
{
    internal static void WriteMainStats(Player<int, long> player)
    {
        Console.WriteLine($"Name: {player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Level: {player.Level}");
        Console.WriteLine($"Experience: {player.Experience}");
        Console.WriteLine($"Class: {player.Class}");
        Console.WriteLine($"Current location: {player.CurrLocation}");
    }
}

internal class PlayerSave : Player<int, long>,  ISaveManageForPlayer
{
    internal void SavePlayer(Player<int, long> player, string filePath)
    {
        try
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonConvert.SerializeObject(player, Formatting.Indented);
            player?.MigrateData();

            File.WriteAllText(filePath, json, Encoding.UTF8);

            Console.WriteLine($"Data saved to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR saving file: {ex.Message}");
            Console.WriteLine($"Full Path: {Path.GetFullPath(filePath)}");
        }
    }
}

internal class PlayerLoad : Player<int, long>, ILoadManageForPlayer
{
    internal Player<int, long>? LoadPlayer(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return null;
        }

        try{
            string json = File.ReadAllText(filePath);
            Player<int, long>? player = JsonConvert.DeserializeObject<Player<int, long>>(json);
            if(player == null)
            {
                Console.WriteLine("Error: File content is invalid.");
                return null;
            }
            Console.WriteLine($"Data is loaded from: {filePath}");
            return player;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return null;
        }
    }
}

internal static class HP
{
    internal static readonly List<int> HPList = new(Levels.Size);
    internal static void Fill(List<int> hpList)
    {
        for(int i = 1; i <= Levels.Size; i++)
        {
            if(i % 2 == 0)
                hpList.Add(i * 500);
            else
                hpList.Add(i * 800);
        }
    }
}

internal class Class : ISaveManageForClasses
{
    [JsonProperty("classesList")]
    internal Dictionary<int, string>? Classes { get; set; } = ClassesStatic;

    internal static Dictionary<int, string>? ClassesStatic { get; set; } = new()
    {
        {1, "Melee"},
        {2, "Range"}
    };

    internal static void InitializeClasses(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var loadedData = JsonConvert.DeserializeObject<Class>(json);
                if(loadedData?.Classes != null)
                {
                    ClassesStatic = loadedData.Classes;
                    Console.WriteLine($"Classes loaded from: {filePath}");
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR loading classes: {ex.Message}");
            }
        }
        Console.WriteLine("Classes file not found. Creating default...");
        SaveClassesStatic(filePath);
    }

    internal static void SaveClassesStatic(string filePath)
    {
        try
        {
            var wrapper = new Class
            {
                Classes = ClassesStatic
            };
            string json = JsonConvert.SerializeObject(wrapper, Formatting.Indented);
            File.WriteAllText(filePath, json, Encoding.UTF8);
            Console.WriteLine($"Default classes saved to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error to save classes: {ex.Message}");
        }
    }
    internal void SaveClasses(Class cl, string filePath)
    {
        if(cl.Classes != null)
        {
            Class.ClassesStatic = cl.Classes;
        }
        SaveClassesStatic(filePath);
    }
}

internal class Levels
{
    internal static readonly int Size = 100;
    internal byte[] Level = new byte[Size];
    internal List<int> ExpPerLevel = new(Size);
    internal static void Fill(byte[] level, List<int> expPerLevel)
    {
        for(int i = 0; i < Size; i++)
        {
            level[i] = (byte)(i + 1);
            if(i % 2 == 0)
                expPerLevel.Add(i * 500 + 500);
            else
                expPerLevel.Add(i * 250 + 250);

        }
    }
}