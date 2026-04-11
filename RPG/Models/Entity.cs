using System.Drawing;
using System.Text;

namespace RPG.Models.Entity
{
    internal partial class Player<I, L> : IInfoAboutPlayer
    {
        private string _str = "";
        [JsonProperty("name")]
        internal string Name 
        { 
            get => FirstUpSymbolToUpper.Apply(_str); 
            set => _str = value;
        }
        [JsonProperty("hp")]
        internal I? HP { get; set; }
        [JsonProperty("level")]
        internal I? Level { get; set; }
        [JsonProperty("experience")]
        internal L? Experience { get; set; }
        [JsonProperty("class")]
        internal string Class { get; set; } = "";
        public Player(){}

        internal void SavePlayer(Player<I, L> player, string filePath)
        {
            try
            {
                string? directory = Path.GetDirectoryName(filePath);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(player, Formatting.Indented);

                File.WriteAllText(filePath, json, Encoding.UTF8);
        
                Console.WriteLine($"Data saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR saving file: {ex.Message}");
                Console.WriteLine($"Full Path: {Path.GetFullPath(filePath)}");
            }
        }

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