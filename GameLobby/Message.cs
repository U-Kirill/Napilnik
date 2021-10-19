namespace GameLobby
{
    public class Message
    {
        public Message(int id, string text, string author)
        {
            Id = id;
            Text = text;
            Author = author;
        }

        public int Id { get; }
        
        public string Text { get; }

        public string Author { get; }

        public override string ToString()
        {
            return $"[{Id}] - {Author}: {Text}";
        }
    }
}