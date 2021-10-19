using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class Player : IPlayer
    {
        private ILobbyConnection _connection;
        private int _lastMessageId;
        

        public bool IsReady => _connection.IsPlayerReady;

        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }

        public void Connect(ILobbyConnection connection)
        {
            _connection = connection;
            _connection.ChatUpdated += ShowMessage;
        }

        public void MakeReady()
        {
            _connection.ApplyCommand(new ReadyCommand());
        }

        public bool CanUseChat()
        {
            CanUseChatCommand command = new CanUseChatCommand();
            _connection.ApplyCommand(command);
            return command.Result;
        }

        public void PrintMessage(string message)
        {
            _connection.ApplyCommand(new PrintMessageCommand(message));
        }

        public IEnumerable<Message> GetUnreadMessage()
        {
            IEnumerable<Message> messages = ReceiveUnreadMessages().ToArray();
            TryRefreshLastMessageId(messages);
            return messages;
        }

        private IEnumerable<Message> ReceiveUnreadMessages()
        {
            var command = new GetUnreadedMessagesCommand(_lastMessageId);
            _connection.ApplyCommand(command);
            IEnumerable<Message> messages = command.Result;
            return messages;
        }

        private void ShowMessage() => 
            GetUnreadMessage().ToList().ForEach(x => Console.WriteLine($"{Name} see: {x}"));

        private void TryRefreshLastMessageId(IEnumerable<Message> messages)
        {
            Message last = messages.LastOrDefault();
            if (last != null)
                _lastMessageId = last.Id;
        }
    }

    public class ReadyCommand : ILobbyCommand
    {
        public void Execute(Player player, Lobby lobby)
        {
            lobby.MakeReady(player);
        }
    }
}