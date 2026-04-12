
namespace RPG.Data.SaveManage
{
    internal static class FileManage
    {
        internal static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        internal static readonly string DirectoryPath = Path.Combine(BaseDirectory, "DataJson");
        internal static readonly string WorldInfoDirectoryPath = Path.Combine(BaseDirectory, "DataJson", "world");
        internal static readonly string ClassesFilePath = Path.Combine(WorldInfoDirectoryPath, "classes.json");
    }
}