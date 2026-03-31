using System.Drawing;

namespace RPG.Models.Entity
{
    internal partial class Player<I, L> : IInfoAboutPlayer
    {
        private string _str = "";
        internal string Name 
        { 
            get => FirstUpSymbolToUpper.Apply(_str); 
            set => _str = value;
        }
        internal I HP { get; set; }
        internal I Level { get; set; }
        internal L Experience { get; set; }
        internal string Class { get; set; } = "";

        internal void SavePlayer(Player<I, L> player, string filePath)
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonConvert.SerializeObject(player, Formatting.Indented);

            File.WriteAllText(filePath, json);
            Console.WriteLine($"Data saved to: {filePath}");
            Console.WriteLine("Click on the button to close.");
            Console.ReadKey();
        }

        internal Player<int, long> LoadPlayer(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            string json = File.ReadAllText(filePath);

            Player<int, long>? player = JsonConvert.DeserializeObject<Player<int, long>>(json);
            Console.WriteLine($"Data is loaded from: {filePath}");
            return player;
        }
    }

    internal class HP
    {
        internal static List<int> HPList = new List<int>(Levels.size);
        internal static void Fill(List<int> hpList)
        {
            for(int i = 0; i < Levels.size; i++)
            {
                if((i += 1) % 2 == 0)
                    hpList.Add(i * 500);
                else
                    hpList.Add(i * 800);
            }
        }
    }

    internal class Class
    {
        internal static Dictionary<int, string> Classes { get; set; } = new Dictionary<int, string>() {{1, "Melee"}, {2, "Range"}};
    }

    internal class Levels
    {
        internal static int size = 100;
        internal byte[] level = new byte[size];
        internal List<int> ExpPerLevel = new List<int>(size);
        internal static void Fill(byte[] level, List<int> expPerLevel)
        {
            for(int i = 0; i < size; i++)
            {
                level[i] = (byte)(i + 1);
                if(i % 2 == 0)
                    expPerLevel.Add(i * 500 + 500);
                else if(i % 2 != 0)
                    expPerLevel.Add(i * 250 + 250);
                
            }
        }
    }
}