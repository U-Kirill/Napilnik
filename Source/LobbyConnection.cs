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
        private readonly Player _player;

        public LobbyConnection(Player player, Lobby lobby)
        {
            _player = player;
            Lobby = lobby;
            _player.StatusChanged += OnPlayerChangeStatus;
        }

        public event Action ChatUpdated;
        public event Action StatusChanged;

        public bool IsPlayerReady => _player.IsReady;

        public IPlayer Player => _player;

        public void PrintMessage(string message) => 
            Lobby.PrintMessage(message, Player);

        public bool CanUseChat()
        {
            return Lobby.CanUseChat(Player);
        }

        public IEnumerable<Message> ReadLastMessage()
        {
            IEnumerable<Message> messages = Lobby.LoadMessage(_lastMessageID, Player).ToArray();
            UpdateLastMessageId(messages);
            return messages;
        }

        public void OnChatUpdate()
        {
            ChatUpdated?.Invoke();
        }

        private void OnPlayerChangeStatus()
        {
            StatusChanged?.Invoke();
        }

        private void UpdateLastMessageId(IEnumerable<Message> messages)
        {
            Message lastMessage = messages.LastOrDefault();
            if(lastMessage != null)
                _lastMessageID = lastMessage.Id;
        }
    }
}