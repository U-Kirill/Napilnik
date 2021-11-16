using GameLobby.Commands.Abstractions;
using GameLobby.Rooms;

namespace GameLobby.Commands
{
    public class ReadyCommand : ILobbyCommand
    {
        public void Execute(Player player, Lobby lobby) =>
            lobby.MakeReady(player);
    }
}