namespace GameLobby.Rooms
{
    public interface ILobby
    {
        int MaxPlayers { get; }
        int ReadyPlayersCount { get; }
        bool HasFreeSlots();
    }
}