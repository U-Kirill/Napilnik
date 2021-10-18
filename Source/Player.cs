namespace Source
{
    public class Player // in lobby will by IPlayer, which cant change IsReady state. But via regular Player is can
        : IPlayer
    {
        public bool IsReady { get; private set; }
        public string Name { get; }
        public ILobbyConnection Connection;

        public Player(string name)
        {
            Name = name;
        }

        public void Connect(ILobbyConnection connection)
        {
            Connection = connection;
        }
    }
}