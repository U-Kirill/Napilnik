using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Source
{
  public class UseCase
  {
    public static void Main(string[] args)
    {
      Game game = new Game();

      Player player1 = new Player();

      game.CreateRoom(player1);
    }
  }

  public class Player
  {

  }

  public class Game
  {
    private List<Room> _rooms;
    private List<Connection> _connections;
    private int _roomsCreated;
    private int _connectionsCreated;

    public Room CreateRoom(Player player)
    {
      Room room = CrateRoom();
      Connect(player, room);

      return room;
    }

    public void Connect(Player player, Room room)
    {
      if (_connections.Exists(x => x.Player == player))
        throw new InvalidOperationException("One of the rooms already contain this player");
      
      if (!_rooms.Contains(room))
        throw new InvalidOperationException("room is not exist");

      player = player ?? throw new NullReferenceException();
      room = room ?? throw new NullReferenceException();
      
      var connection = new Connection(GetNextConnectionId(), player, room);
      
      _connections.Add(connection);
      
      room.Add(connection);
    }
    
    public void 

    private Room CrateRoom()
    {
      var room = new Room(GetNextRoomId());
      _rooms.Add(room);
      return room;
    }

    private int GetNextRoomId() =>
      _roomsCreated++;
    
    private int GetNextConnectionId() =>
      _connectionsCreated++;

    private class Connection : IConnection
    {
      public Connection(int id, Player player, Room room)
      {
        Id = id;
        Player = player;
        Room = room;
      }
      
      public Player Player { get; }
      public Room Room { get; }
      public int Id { get; }
      public bool IsPlayerReady { get; private set; }

      public void MakeReady(Player player)
      {
        if (player != Player)
          throw new InvalidOperationException();

        IsPlayerReady = true;
      }
    }
  }


  public class Room
  {
    private readonly int _roomId;
    private readonly List<IConnection> _connection = new List<IConnection>();

    public Room(int roomId)
    {
      _roomId = roomId;
    }

    public void Add(IConnection connection)
    {
      connection = connection ?? throw new NullReferenceException();
      
      _connection.Add(connection);
    }

    public IEnumerable<Player> Players => _connection.Select(x => x.Player);
    
  }
}