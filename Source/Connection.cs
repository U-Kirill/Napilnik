using System;

namespace Source
{
  public class Connection : IConnection
  {
    public Connection(Player player, IRoom room)
    {
      Player = player;
      Room = room;
    }

    public event Action<Message> MessageReceived;
    public Player Player { get; }
    public IRoom Room { get; }
    public bool IsPlayerReady { get; private set; }

    public void MakeReady(Player player)
    {
      if (player != Player)
        throw new InvalidOperationException();

      IsPlayerReady = true;
    }

    public void ReceiveMessage(Message message) =>
      MessageReceived?.Invoke(message);

  }
}