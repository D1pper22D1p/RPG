
namespace RPG.Core;

internal static class StartGame
{
    internal static void Main(string[] args)
    {
        string directory = FileManage.DirectoryPath;
        string directoryWorld = FileManage.WorldInfoDirectoryPath;
        string classesFile = FileManage.ClassesFilePath;
        string itemsFile = FileManage.ItemsFilePath;

        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        if (!Directory.Exists(directoryWorld)) Directory.CreateDirectory(directoryWorld);

        Class.InitializeClasses(classesFile);
        SaveLoadAllNecessaryData.Saves(itemsFile);

        string[] saveWFiles = Directory.GetFiles(directoryWorld, "*.json");
        string[] saveFiles = Directory.GetFiles(directory, "*.json");
        if (saveWFiles == null)
        {
            _ = new Class();
        }

        PlayerSave playerSave = new();
        PlayerLoad playerLoad = new();

        Player<int, long>? player = null;

        if (saveFiles.Length > 0)
        {
            Console.WriteLine("--- Saved Games Found ---");
            for (int i = 0; i < saveFiles.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(saveFiles[i]);
                Console.WriteLine($"{i + 1}. {fileName}");
            }

            Console.WriteLine("0. Create New Character");
            Console.Write("Select option: ");
            string? choice = Console.ReadLine();

            if (!string.IsNullOrEmpty(choice) && int.TryParse(choice, out int index) && index > 0 && index <= saveFiles.Length)
            {
                player = playerLoad.LoadPlayer(saveFiles[index - 1]);
                if (player == null)
                {
                    Console.WriteLine("Error loading file. Creating new character...");
                    player = CreateNewPlayer(directory, playerSave);
                }
                else
                {
                    Console.WriteLine($"Loaded: {player.Name}");
                }
            }
            else
            {
                player = CreateNewPlayer(directory, playerSave);
            }
        }
        else
        {
            Console.WriteLine("No saved games found. Creating new character...");
            player = CreateNewPlayer(directory, playerSave);
        }

        if (player != null)
        {
            Console.WriteLine("\n--- Player Stats ---");
            PlayerInfo.WriteMainStats(player);

            string savePath = Path.Combine(directory, $"{player.Name}.json");
            playerSave.SavePlayer(player, savePath);
        }

        Console.ReadKey();
    }

    private static Player<int, long> CreateNewPlayer(string directory, PlayerSave playerSave)
    {
        Player<int, long> newPlayer = new();
        ((IInputInfoAboutPlayer)newPlayer).InputMainStats(newPlayer);

        string savePath = Path.Combine(directory, $"{newPlayer.Name}.json");
        playerSave.SavePlayer(newPlayer, savePath);

        return newPlayer;
    }
}

internal static class SaveLoadAllNecessaryData
{
    public static void Saves(string filePath)
    {
        SaveConsumableItems.SaveItemsList(filePath);
    }
}