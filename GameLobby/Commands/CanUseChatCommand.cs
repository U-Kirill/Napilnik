using GameLobby.Commands.Abstractions;
using GameLobby.Rooms;

namespace GameLobby.Commands
{
    public class CanUseChatCommand : IReturnableLobbyCommand<bool>
    {
        public bool Result { get; private set; }

        public void Execute(Player player, Lobby lobby) => 
            Result = lobby.CanUseChat(player);
    }
}