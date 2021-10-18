using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class Chat
    {
        private readonly List<Message> _messages = new List<Message>();
        private int _messagesCount = 0;

        public IReadOnlyList<Message> Messages => _messages;

        public void Add(string message, IPlayer player)
        {
            _messages.Add(new Message(_messagesCount, message, player.Name));
            _messagesCount++;
        }

        public IEnumerable<Message> LoadSince(int lastMessageId) => 
            _messages.SkipWhile(x => x.Id != lastMessageId);
    }
}