using System.Runtime.InteropServices.ComTypes;

namespace Source
{
  public class UseCase
  {

    public static void Main(string[] args)
    {
      Game game = new Game();
      Rooms rooms = game.Rooms;

      Player player1 = new Player("Noob");
      IRoom room1 = rooms.CreateRoomAndConnect(player1, 10);

      Player player2 = new Player("Pro");
      rooms.Connect(player2, room1);
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
    
    public Rooms Rooms { get; } = new Rooms();
  }
}