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
        void ApplyCommand(ILobbyCommand command);
    }

    public class LobbyConnection : ILobbyConnection
    {
        private int _lastMessageID;
        
        private readonly Lobby Lobby;
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

        public void ApplyCommand(ILobbyCommand command)
        {
            command.Execute(_player, Lobby);
        }
        
        public void PrintMessage(string message) =>
            Lobby.PrintMessage(message, Player);

        public bool CanUseChat() => 
            Lobby.CanUseChat(Player);

        public void OnChatUpdate() => 
            ChatUpdated?.Invoke();

        private void OnPlayerChangeStatus() => 
            StatusChanged?.Invoke();

        public IEnumerable<Message> ReadLastMessage()
        {
            IEnumerable<Message> messages = Lobby.LoadMessage(_lastMessageID, Player).ToArray();
            UpdateLastMessageId(messages);
            return messages;
        }

        private void UpdateLastMessageId(IEnumerable<Message> messages)
        {
            Message lastMessage = messages.LastOrDefault();
            if(lastMessage != null)
                _lastMessageID = lastMessage.Id;
        }
    }

    public interface ILobbyCommand
    {
        void Execute(Player player, Lobby lobby);
    }
    
    public interface IReturnableLobbyCommand<T> : ILobbyCommand
    {
        T Result { get; }
    }

    public class CanUseChatCommand : IReturnableLobbyCommand<bool>
    {
        public bool Result { get; private set; }

        public void Execute(Player player, Lobby lobby)
        {
            Result = lobby.CanUseChat(player);
        }
    }
    
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

    public class PrintMessageCommand : ILobbyCommand
    {
        private string _message;

        public PrintMessageCommand(string message)
        {
            _message = message;
        }

        public void Execute(Player player, Lobby lobby) => 
            lobby.PrintMessage(_message, player);
    }
}