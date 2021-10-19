using System;
using System.Collections.Generic;
using System.Linq;
using GameLobby.Commands;

namespace GameLobby
{
    public class Player : IPlayer
    {
        private readonly IMessageView _messageView;

        private ILobbyConnection _connection;
        private int _lastMessageId;

        public Player(string name, IMessageView messageView)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            Name = name;
            _messageView = messageView ?? throw new ArgumentNullException();
        }

        public string Name { get; }

        public void Connect(ILobbyConnection connection)
        {
            _connection = connection;
            _connection.ChatUpdated += ShowMessage;
            ShowMessage();
        }

        public void MakeReady()
        {
            ValidateConnect();
            _connection.ApplyCommand(new ReadyCommand());
        }

        public bool CanUseChat()
        {
            ValidateConnect();
            CanUseChatCommand command = new CanUseChatCommand();
            _connection.ApplyCommand(command);
            return command.Result;
        }

        public void PrintMessage(string message)
        {
            ValidateConnect();
            _connection.ApplyCommand(new PrintMessageCommand(message));
        }

        private void ShowMessage()
        {
            ValidateConnect();
            GetUnreadMessage().ToList().ForEach(x => _messageView.Show(x));
        }

        private void ValidateConnect()
        {
            if (_connection == null)
                throw new InvalidOperationException();
        }

        private IEnumerable<Message> GetUnreadMessage()
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

        private void TryRefreshLastMessageId(IEnumerable<Message> messages)
        {
            Message last = messages.LastOrDefault();
            if (last != null)
                _lastMessageId = last.Id;
        }
    }
}