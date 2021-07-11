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

      var room = new Room(GetNextRoomId(), maxPlayers);
      _rooms.Add(room);
      
      return room;
    }

    public IReadOnlyList<IRoom> Rooms => _rooms;

    public IConnection Connect(Player player, IRoom room)
    {
      player = player ?? throw new NullReferenceException(nameof(player));
      room = room ?? throw new NullReferenceException(nameof(room));

      if (IsRoomExistPlayer(player))
        throw new InvalidOperationException("One of the rooms already contain this player");

      Room targetRoom = _rooms.FirstOrDefault(x => x == room)
                        ?? throw new InvalidOperationException("room is not exist");

      return targetRoom.Connect(player);
    }

    private bool IsRoomExistPlayer(Player player) =>
      _rooms.Exists(room => room.Connections.Any(x => x.Player == player));

    private int GetNextRoomId() =>
      _roomsCreated++;

    private class Room : IRoom
    {
      private readonly int _roomId;
      private readonly List<Connection> _connections = new List<Connection>();
      private readonly Chat _chat = new Chat();

      private State _state;

      public Room(int roomId, int maxPlayers)
      {
        MaxPlayers = maxPlayers;
        _roomId = roomId;
        _chat.MessageRecived += InformActivePlayers;

        _state = new WaitPlayerState(this);
      }

      public int MaxPlayers { get; }
      public IReadOnlyList<IPlayerConnection> Connections => _connections;
      
      private bool HasFreeSlots => ReadyPlayersCount < MaxPlayers;
      private int ReadyPlayersCount => _connections.Count(x => x.IsPlayerReady);

      public IConnection Connect(Player player)
      {
        player = player ?? throw new NullReferenceException();

        if (!HasFreeSlots)
          throw new InvalidOperationException("Reached Max Ready Players count");
        
        var connection = new Connection(player, this);
        _connections.Add(connection);

        return connection;
      }

      public void MakeReady(Player player)
      {
        player = player ?? throw new NullReferenceException(nameof(player));
        
        if (!IsActivePlayer(player))
          throw new InvalidOperationException("Player is not active");

        if (!HasFreeSlots)
          throw new InvalidOperationException("Reached Max Ready Players count");
        
        Connection connectionWithPlayer = _connections
          .FirstOrDefault(x => x.Player == player);
        
        if(connectionWithPlayer.IsPlayerReady)
          throw new InvalidOperationException("Player already ready");
          
        connectionWithPlayer.MakeReady(player);

        if (CanStartGame())
          StartGame();
      }

      public void SendMessage(Player sender, string message)
      {
        if (!IsActivePlayer(sender))
          throw new InvalidOperationException("Is not active player");

        _chat.Write(sender, message);
      }

      public bool IsActivePlayer(Player sender) =>
        _state.ActiveConnection.Any(x => x.Player == sender);

      private void InformActivePlayers(Message message)
      {
        foreach (Connection connection in _state.ActiveConnection)
          connection.ReceiveMessage(message);
      }
      
      private void StartGame() => 
        _state = new GameState(this);

      private bool CanStartGame() => 
        ReadyPlayersCount == MaxPlayers;
      
      private abstract class State
      {
        public State(Room room)
        {
          Room = room;
        }

        public abstract IReadOnlyList<Connection> ActiveConnection { get; }

        protected Room Room { get; }
      }

      private class WaitPlayerState : State
      {
        public WaitPlayerState(Room room)
            : base(room) { }

        public override IReadOnlyList<Connection> ActiveConnection => Room._connections;
      }

      private class GameState : State
      {
        public GameState(Room room)
          : base(room) { }

        public override IReadOnlyList<Connection> ActiveConnection =>
          Room._connections.Where(x => x.IsPlayerReady).ToList();
      }
    }
  }
}