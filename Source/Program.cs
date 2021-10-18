using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
  public class Program
  {
    public static void Main()
    {
      Game game = new Game();
    }
  }

  public class Game
  {
    public List<Lobby> Lobbies = new List<Lobby>();
  }

  public class Lobby
  {
    private readonly int _maxPlayers;
    private readonly List<IPlayer> Players = new List<IPlayer>();
    
    public Lobby(int maxPlayers)
    {
      _maxPlayers = maxPlayers;
    }

    private IEnumerable<IPlayer> ReadyPlayers => Players.Where(x => x.IsReady);
    
    public void Connect(IPlayer player)
    {
      if (player == null)
        throw new ArgumentException();

      if (ReadyPlayers.Count() == _maxPlayers)
        throw new InvalidOperationException();

      Players.Add(player);
    }
  }

  public interface IPlayer
  {
    bool IsReady { get; }
  }

  public class Player // in lobby will by IPlayer, which cant change IsReady state. But via regular Player is can
    : IPlayer
  {
    public bool IsReady { get; private set; }
  }
}