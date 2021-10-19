using System;

namespace GameLobby
{
    public class ConsoleMessageView : IMessageView
    {
        private readonly string _title;

        public ConsoleMessageView()
        {
            _title = String.Empty;
        }

        public ConsoleMessageView(string title)
        {
            _title = title + ": ";
        }

        public void Show(Message message) => 
            Console.WriteLine($"{_title}{message}");
    }
}