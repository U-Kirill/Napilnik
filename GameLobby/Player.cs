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
                throw new ArgumentNullException(nameof(name));

            Name = name;
            _messageView = messageView ?? throw new ArgumentNullException(nameof(messageView));
        }

        public string Name { get; }

        public bool IsConnected => _connection != null;

        public void Connect(ILobbyConnection connection)
        {
            if (IsConnected)
                throw new InvalidOperationException("Player already connected");

            _connection = connection;
            _connection.ChatUpdated += ShowMessage;
            ShowMessage();
        }

        public bool CanMakeReady()
        {
            ValidateConnect();

            var command = new CanMakeReadyCommand();
            _connection.ApplyCommand(command);
            return command.Result;
        }

        public void MakeReady()
        {
            ValidateConnect();
            _connection.ApplyCommand(new ReadyCommand());
        }

        public bool CanUseChat()
        {
            ValidateConnect();

            var command = new CanUseChatCommand();
            _connection.ApplyCommand(command);
            return command.Result;
        }

        public void SendMessage(string message)
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
            if (!IsConnected)
                throw new InvalidOperationException("Player not connected");
        }

        private IEnumerable<Message> GetUnreadMessage()
        {
            IEnumerable<Message> messages = ReceiveUnreadMessages().ToArray();
            TryRefreshLastMessageId(messages);
            return messages;
        }

        private IEnumerable<Message> ReceiveUnreadMessages()
        {
            var command = new GetUnreadMessagesCommand(_lastMessageId);
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