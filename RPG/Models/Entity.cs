using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;

namespace RPG.Models.Entity
{
    internal partial class Player<I, L> : IInfoAboutPlayer
    {
        private string _str = "";
        private byte _dataVersion = 1;
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
        [JsonProperty("location")]
        internal string currLocation { get; set; } = "";
        public Player(){}

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (string.IsNullOrEmpty(currLocation))
                currLocation = "Forest";
        }

        internal void MigrateData()
        {
            if(_dataVersion < 1)
            {
                currLocation = "Forest";
                _dataVersion += 1;
            }
        }

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
        [JsonProperty("classesList")]
        internal Dictionary<int, string>? Classes { get; set; } = classes;

        internal static Dictionary<int, string>? classes { get; set; } = new Dictionary<int, string>() 
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
                        classes = loadedData.Classes;
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
                var wrapper = new Class();
                wrapper.Classes = classes;
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
                Class.classes = cl.Classes;
            }
            SaveClassesStatic(filePath);
        }
        /*internal Class? LoadClasses(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            try{
                string json = File.ReadAllText(filePath);
                Class? classes = JsonConvert.DeserializeObject<Class>(json);
                if(classes == null)
                {
                    Console.WriteLine("Error: File content is invalid.");
                    return null;
                }
                Console.WriteLine($"Data is loaded from: {filePath}");
                return classes;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }*/
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