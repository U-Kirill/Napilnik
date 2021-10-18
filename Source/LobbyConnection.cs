using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public interface ILobbyConnection
    {
        bool IsPlayerReady { get; }
        event Action ChatUpdated;
        void PrintMessage(string message);
        IEnumerable<Message> ReadLastMessage();
        bool CanUseChat();
    }

    public class LobbyConnection : ILobbyConnection
    {
        private readonly Lobby Lobby;
        private int _lastMessageID;

        public LobbyConnection(Player player, Lobby lobby)
        {
            Player = player;
            Lobby = lobby;
        }

        public event Action ChatUpdated;

        public bool IsPlayerReady => Player.IsReady;

        public IPlayer Player { get; }

        public void PrintMessage(string message) => 
            Lobby.PrintMessage(message, Player);

        public bool CanUseChat()
        {
            return Lobby.CanUseChat(Player);
        }

        public IEnumerable<Message> ReadLastMessage()
        {
            IEnumerable<Message> messages = Lobby.LoadMessage(_lastMessageID, Player);
            _lastMessageID = messages.Last().Id;
            return messages;
        }

        public void OnChatUpdate()
        {
            ChatUpdated?.Invoke();
        }
    }
}