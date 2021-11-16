using System;
using GameLobby.Rooms;

namespace GameLobby
{
  public class Program
  {
    public static void Main()
    {
      // Use case for a simpler understanding
      Game game = new Game();
      ILobby lobby = game.Create(2);

      Player igor = new Player("Igor", new ConsoleMessageView("Igor see"));
      Player valera = new Player("Valera", new ConsoleMessageView("Valera see"));
      Player anatoliy = new Player("Anatoliy", new ConsoleMessageView("Anatoliy see"));

      TryConnect(lobby, game, igor); // will
      TryConnect(lobby, game, valera); // will
      TryConnect(lobby, game, anatoliy); // will

      igor.SendMessage("Hello! Set ready, Valera");
      TryMakeReady(igor); // ok 1/2

      valera.SendMessage("Hi, Igor! One moment..");

      // a few hours later
      TryMakeReady(valera); // ok 2/2 - game state
      valera.SendMessage("Yes, i'm ready. Lets play.");

      if (anatoliy.CanUseChat()) // false
        anatoliy.SendMessage("Hi, bodies ;)"); // will not send.

      TryMakeReady(anatoliy); // nope. lobby has max ready players

      Console.ReadLine();
    }

    private static void TryMakeReady(Player player)
    {
      if (player.CanMakeReady())
        player.MakeReady();
    }

    // poor method for demo only
    private static void TryConnect(ILobby lobby, Game game, Player player)
    {
      if (lobby.HasFreeSlots())
        game.Connect(player, lobby);
    }

    /*
      Igor see: [1] - Igor: Hello! Set ready, Valera
      Valera see: [1] - Igor: Hello! Set ready, Valera
      Anatoliy see: [1] - Igor: Hello! Set ready, Valera

      Igor see: [2] - Valera: Hi, Igor! One moment..
      Valera see: [2] - Valera: Hi, Igor! One moment..
      Anatoliy see: [2] - Valera: Hi, Igor! One moment..

      Igor see: [3] - Server: Game Started!
      Valera see: [3] - Server: Game Started!

      Igor see: [4] - Valera: Yes, i'm ready. Lets play.
      Valera see: [4] - Valera: Yes, i'm ready. Lets play.
     */
  }
}