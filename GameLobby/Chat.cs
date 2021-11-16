using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLobby
{
    public class Chat
    {
        private readonly List<Message> _messages = new List<Message>();
        private int _messagesCount;

        public IReadOnlyList<Message> Messages => _messages;

        public void Add(string message, string author)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentNullException(nameof(author));

            _messagesCount++;
            _messages.Add(new Message(_messagesCount, message, author));
        }

        public IEnumerable<Message> LoadSince(int lastMessageId) =>
            _messages.SkipWhile(x => x.Id <= lastMessageId);
    }
}