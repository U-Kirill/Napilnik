using System.Runtime.InteropServices.ComTypes;

namespace Source
{
  public class UseCase
  {

    public static void Main(string[] args)
    {
      Game game = new Game();
      Lobby lobby = game.Lobby;

      Player player1 = new Player("Noob");
      IRoom room1 = lobby.CreateRoom(10);

      Player player2 = new Player("Pro");
      lobby.Connect(player2, room1);
    }

  }

  public class Player
  {

    public Player(string name)
    {
      Name = name;
    }

    public string Name { get; }

  }

  public class Game
  {
    
    public Lobby Lobby { get; } = new Lobby();
  }
}