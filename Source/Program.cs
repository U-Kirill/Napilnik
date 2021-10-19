using System;
using System.Linq;

namespace Source
{
  public class Program
  {
    public static void Main()
    {
      Game game = new Game();
      ILobby lobby = game.Create();
      Player player1 = new Player("player 1");
      
      Console.WriteLine($"can connect {lobby.HasFreeSlots()}");
      game.Connect(player1, lobby);
      Console.WriteLine($"ReadyPlayersCount {lobby.ReadyPlayersCount}");
      player1.MakeReady();
      Console.WriteLine($"ReadyPlayersCount {lobby.ReadyPlayersCount}");
      //player1.Connection.ChatUpdated += () => player1.GetUnreadMessage().ToList().ForEach(x => Console.WriteLine($"reactivity: [{x.Id}] - {x.Author}: {x.Text}"));
      player1.PrintMessage("Hello from player 1");
      player1.PrintMessage("What's up");

      var player2 = new Player("player 2");
      game.Connect(player2, lobby);
      Console.WriteLine("How player 2 sees a chat:");
      player2.GetUnreadMessage().ToList().ForEach(x => Console.WriteLine($"[{x.Id}] - {x.Author}: {x.Text}"));

      var player3 = new Player("player 3");
      game.Connect(player3, lobby);
      player3.PrintMessage("Hello from player 3");
      
      var player4 = new Player("player 4");
      game.Connect(player4, lobby);
      player4.PrintMessage("Hello from player 4");
      
      Console.WriteLine("How player 4 sees a chat:");
      player4.GetUnreadMessage().ToList().ForEach(x => Console.WriteLine($"[{x.Id}] - {x.Author}: {x.Text}"));

      Console.WriteLine($"Try What's up from 4");
      player4.PrintMessage("What's up from 4");

      Console.WriteLine($"make ready");
      player2.MakeReady();
      
      Console.WriteLine($"Try What's up from 4");
      player4.PrintMessage("What's up from 4");
      
      Console.WriteLine($"Try print from 1 and read from 4");
      player1.PrintMessage("Hello from player 1");
      player4.GetUnreadMessage().ToList().ForEach(x => Console.WriteLine($"[{x.Id}] - {x.Author}: {x.Text}"));
      
      Console.WriteLine($"Try print from 1 and read from 2");
      player1.PrintMessage("Hello from player 1");
      player2.GetUnreadMessage().ToList().ForEach(x => Console.WriteLine($"[{x.Id}] - {x.Author}: {x.Text}"));
      
      Console.ReadLine();
    }
  }

  public interface ILobby
  {
    int MaxPlayers { get; }
    int ReadyPlayersCount { get; }
    bool HasFreeSlots();
  }

  
  public interface IPlayer
  {
    string Name { get; }
  }
}