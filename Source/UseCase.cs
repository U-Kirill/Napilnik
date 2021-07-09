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

      Player player1 = new Player("Noob");
      IRoom room1 = game.CreateRoomAndConnect(player1);

      Player player2 = new Player("Pro");
      game.Connect(player2, room1);
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

    private List<Room> _rooms;
    private int _roomsCreated;
    private int _connectionsCreated;

    public IRoom CreateRoomAndConnect(Player player)
    {
      Room room = CrateRoom();
      Connect(player, room);

      return room;
    }

    public void Connect(Player player, Room room)
    {
      player = player ?? throw new NullReferenceException();
      room = room ?? throw new NullReferenceException();

      if (!_rooms.Contains(room))
        throw new InvalidOperationException("room is not exist");

      if (_rooms.Exists(room => room.Players.Contains(player)))
        throw new InvalidOperationException("One of the rooms already contain this player");

      room.Add(player);
    }

    private Room CrateRoom()
    {
      var room = new Room(GetNextRoomId());
      _rooms.Add(room);
      return room;
    }

    private int GetNextRoomId() =>
      _roomsCreated++;

  }


  public class Romms
  {
    private List<Room> _rooms;
    private int _roomsCreated;

    public IRoom CreateRoomAndConnect(Player player)
    {
      Room room = CrateRoom();
      Connect(player, room);

      return room;
    }

    public IReadOnlyList<IRoom> Rooms => _rooms;
    
    public void Connect(Player player, IRoom room)
    {
      player = player ?? throw new NullReferenceException();
      room = room ?? throw new NullReferenceException();

      if (_rooms.Exists(room => room.Players.Contains(player)))
        throw new InvalidOperationException("One of the rooms already contain this player");
      
      Room targetRoom = _rooms.FirstOrDefault(x => x == room)
                        ?? throw new InvalidOperationException("room is not exist");

      targetRoom.Add(player);
    }

    private Room CrateRoom()
    {
      var room = new Room(GetNextRoomId());
      _rooms.Add(room);
      return room;
    }

    private int GetNextRoomId() =>
      _roomsCreated++;
    
    private class Room : IRoom
    {

      private readonly int _roomId;
      private readonly List<Connection> _connection = new List<Connection>();
      private Chat _chat;

      public Room(int roomId)
      {
        _roomId = roomId;
      }

      public void Add(Player player)
      {
        player = player ?? throw new NullReferenceException();

        _connection.Add(new Connection(player));
      }

      public void MakeReady(Player player)
      {
        _connection
          .First(x => x.Player == player)
          .MakeReady(player);
      }

      // при смене состояния изменять стейт подборщика коннектов. И результат кидать в исключенте
      public void SendMessage(Player sender, string message) =>
        _chat.AddText(sender.Name, message);

      public string ReadAllMessages(Player reader) =>
        _chat.Messages.Aggregate((x, accum) => accum += x + "\n");

      public IEnumerable<Player> Players => _connection.Select(x => x.Player);


      private class Connection : IConnection
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

  }
}

internal class Chat
  {

    public void AddText(string senderName, string message)
    {
      throw new NotImplementedException();
    }

    public string[] Messages { get; set; }

  }
}