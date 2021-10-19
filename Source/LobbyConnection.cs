using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public interface ILobbyConnection
    {
        event Action ChatUpdated;

        bool IsPlayerReady { get; }

        void ApplyCommand(ILobbyCommand command);
    }

    public class LobbyConnection : ILobbyConnection
    {
        private readonly Lobby _lobby;
        private readonly Player _player;

        public LobbyConnection(Player player, Lobby lobby)
        {
            _player = player;
            _lobby = lobby;
        }

        public event Action ChatUpdated;

        public bool IsPlayerReady { get; private set; }

        public IPlayer Player => _player;

        public void ApplyCommand(ILobbyCommand command)
        {
            command.Execute(_player, _lobby);
        }

        public void OnChatUpdate() => 
            ChatUpdated?.Invoke();

        public void MakeReady() => 
            IsPlayerReady = true;
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