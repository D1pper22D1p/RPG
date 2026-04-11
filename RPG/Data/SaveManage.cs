
namespace RPG.Data.SaveManage
{
    internal class fileManage
    {
        protected internal static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        protected internal static string directoryPath = Path.Combine(baseDirectory, "DataJson");
        protected internal static string worldInfoDirectoryPath = Path.Combine(baseDirectory, "DataJson", "world");
        protected internal static string classesFilePath = Path.Combine(worldInfoDirectoryPath, "classes.json");
    }
}