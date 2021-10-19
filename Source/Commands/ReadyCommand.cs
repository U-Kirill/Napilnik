namespace Source
{
    public class ReadyCommand : ILobbyCommand
    {
        public void Execute(Player player, Lobby lobby)
        {
            lobby.MakeReady(player);
        }
    }
}