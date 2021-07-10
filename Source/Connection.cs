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

    public Player Player { get; }
    public IRoom Room { get; }
    public bool IsPlayerReady { get; private set; }

    public void MakeReady(Player player)
    {
      if (player != Player)
        throw new InvalidOperationException();

      IsPlayerReady = true;
    }

  }
}