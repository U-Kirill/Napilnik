using System;

namespace Source
{
  public class Connection : IConnection
  {

    public Connection(Player player)
    {
      Player = player;
    }

    public Player Player { get; }
    public bool IsPlayerReady { get; private set; }

    public void MakeReady(Player player)
    {
      if (player != Player)
        throw new InvalidOperationException();

      IsPlayerReady = true;
    }

  }
}