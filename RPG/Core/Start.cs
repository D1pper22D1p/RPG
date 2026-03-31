
namespace RPG.Core.Start
{
    internal class StartGame
    {
        internal static void Main(string[] args)
        {
            Player<int, long> player = new Player<int, long>();
            if(player.LoadPlayer(fileManage.directoryPath) == null)
                ((IInfoAboutPlayer)player).InputMainStats(player);
            do
            {
                ((IInfoAboutPlayer)player).WriteMainStats(player);
                player.SavePlayer(player, fileManage.directoryPath);
                break;
            }while(true);
        }
    }
}