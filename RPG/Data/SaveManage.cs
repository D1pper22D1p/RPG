
using System.Reflection.Metadata;

namespace RPG.Data;

internal static class FileManage
{
    internal static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    internal static readonly string DirectoryPath = Path.Combine(BaseDirectory, "DataJson");
    internal static readonly string WorldInfoDirectoryPath = Path.Combine(BaseDirectory, "DataJson", "world");
    internal static readonly string ClassesFilePath = Path.Combine(WorldInfoDirectoryPath, "classes.json");
    internal static readonly string InventoryDirectoryPath = Path.Combine(BaseDirectory, "Inventory");
    internal static readonly string InventoryFilePath = Path.Combine(InventoryDirectoryPath, "inventory.json");
    internal static readonly string ItemsFilePath = Path.Combine(WorldInfoDirectoryPath, "items.json");
}
internal interface ISaveManageForClasses
{
    internal static virtual void SaveClassesStatic(string filePath) {}
    internal virtual void SaveClasses(Class cl, string filePath) {}
}
internal interface ISaveManageForPlayer 
{
    internal virtual void SavePlayer(Player<int, long> player, string filePath) {}
}
internal interface ILoadManageForPlayer
{
    internal virtual Player<int, long>? LoadPlayer(string filePath) 
    {
        Player<int, long> player = new();
        return player;
    }
}
internal interface ISaveManageForItems
{
    internal virtual void SaveItemsList(string filePath) {}
}