using System;

namespace Source
{
  public class Connection : IConnection
  {
    public Connection(Player player, IRoom room)
    {
      player = player ?? throw new NullReferenceException(nameof(player));
      room = room ?? throw new NullReferenceException(nameof(room));
      
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
        throw new InvalidOperationException("Player in connection not equals");

      IsPlayerReady = true;
    }

    public void ReceiveMessage(Message message)
    {
      message = message ?? throw new NullReferenceException(nameof(message));
      
      MessageReceived?.Invoke(message);
    }
  }
}