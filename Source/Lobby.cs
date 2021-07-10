using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
  public class Lobby
  {
    private List<Room> _rooms = new List<Room>();
    private int _roomsCreated;

    public IRoom CreateRoom(int maxPlayers)
    {
      if (maxPlayers <= 0)
        throw new ArgumentOutOfRangeException(nameof(maxPlayers));

      return CrateRoom(maxPlayers);
    }

    public IReadOnlyList<IRoom> Rooms => _rooms;

    public IConnection Connect(Player player, IRoom room)
    {
      player = player ?? throw new NullReferenceException(nameof(player));
      room = room ?? throw new NullReferenceException(nameof(room));

      if (_rooms.Exists(room => room.Players.Contains(player)))
        throw new InvalidOperationException("One of the rooms already contain this player");
      
      Room targetRoom = _rooms.FirstOrDefault(x => x == room)
                        ?? throw new InvalidOperationException("room is not exist");

      return targetRoom.Add(player);
    }

    private Room CrateRoom(int maxPlayers)
    {
      var room = new Room(GetNextRoomId(), maxPlayers);
      _rooms.Add(room);
      return room;
    }

    private int GetNextRoomId() =>
      _roomsCreated++;

    private class Room : IRoom
    {

      private readonly int _roomId;
      private readonly List<Connection> _connection = new List<Connection>();
      private Chat _chat = new Chat();

      public Room(int roomId, int maxPlayers)
      {
        MaxPlayers = maxPlayers;
        _roomId = roomId;
        _chat.MessageRecived += Inform;
      }

      private void Inform(Message obj)
      {
        throw new NotImplementedException();
      }

      public int MaxPlayers { get; }

      public IEnumerable<Player> Players => _connection.Select(x => x.Player);

      public int ReadyPlayersCount => _connection.Count(x => x.IsPlayerReady);

      public IConnection Add(Player player)
      {
        player = player ?? throw new NullReferenceException();

        var connection = new Connection(player, this);
        _connection.Add(connection);

        return connection;
      }

      public void MakeReady(Player player)
      {
        player = player ?? throw new NullReferenceException(nameof(player));

        Connection connectionWithPlayer = _connection
          .FirstOrDefault(x => x.Player == player);

        if (connectionWithPlayer == null)
          throw new InvalidOperationException("Can't make ready player which not exist in this room");
        
        connectionWithPlayer.MakeReady(player);
      }

      // при смене состояния изменять стейт подборщика коннектов. И результат кидать в исключенте

      public void SendMessage(Player sender, string message) =>
        _chat.Write(sender.Name, message);
      
    }

  }
}