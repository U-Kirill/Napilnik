using System.Collections.Generic;
using GameLobby.Commands.Abstractions;
using GameLobby.Rooms;

namespace GameLobby.Commands
{
    public class GetUnreadedMessagesCommand : IReturnableLobbyCommand<IEnumerable<Message>>
    {
        private int _lastMessageId;

        public GetUnreadedMessagesCommand(int lastMessageId)
        {
            _lastMessageId = lastMessageId;
        }

        public IEnumerable<Message> Result { get; private set; }

        public void Execute(Player player, Lobby lobby)
        {
            Result = lobby.LoadMessage(_lastMessageId,player);
        }
    }
}