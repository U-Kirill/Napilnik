using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class Player // in lobby will by IPlayer, which cant change IsReady state. But via regular Player is can
        : IPlayer
    {
        public event Action StatusChanged;
        
        public bool IsReady { get; private set; }
        
        public string Name { get; }
        
        public ILobbyConnection Connection;
        private int _lastMessageId;

        public Player(string name)
        {
            Name = name;
        }

        public void PrintMessage(string message)
        {
            Connection.ApplyCommand(new PrintMessageCommand(message));
        }
        
        public bool CanUseChat()
        {
            CanUseChatCommand command = new CanUseChatCommand();
            Connection.ApplyCommand(command);
            return command.Result;
        }

        public IEnumerable<Message> GetUnreaded()
        {
            var command = new GetUnreadedMessagesCommand(_lastMessageId);
            Connection.ApplyCommand(command);
            IEnumerable<Message> messages = command.Result;
            TryRefreshLastMessageId(messages);
            return messages;
        }

        private void TryRefreshLastMessageId(IEnumerable<Message> messages)
        {
            Message last = messages.LastOrDefault();
            if (last != null)
                _lastMessageId = last.Id;
        }

        public void Connect(ILobbyConnection connection)
        {
            Connection = connection;
            IsReady = false;
        }

        public void MakeReady()
        {
            IsReady = true;
            StatusChanged?.Invoke();
        }
    }
}