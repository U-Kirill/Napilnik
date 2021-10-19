namespace Source
{
    public interface ILobbyCommand
    {
        void Execute(Player player, Lobby lobby);
    }
}