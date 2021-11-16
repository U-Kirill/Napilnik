using GameLobby.Rooms;

namespace GameLobby.Commands.Abstractions
{
    public interface ILobbyCommand
    {
        void Execute(Player player, Lobby lobby);
    }
}