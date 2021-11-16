using System;

namespace GameLobby
{
    public class ConsoleMessageView : IMessageView
    {
        private readonly string _title;

        public ConsoleMessageView()
        {
            _title = string.Empty;
        }

        public ConsoleMessageView(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException($"{nameof(title)} is null. Use constructor without arguments");

            _title = title + ": ";
        }

        public void Show(Message message) =>
            Console.WriteLine($"{_title}{message}");
    }
}