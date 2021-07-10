using System;
using System.Linq;
using NUnit.Framework;
using Source;

namespace GameLobbyTest
{
  public class Tests
  {

    private Rooms _rooms;

    [SetUp]
    public void Setup()
    {
      Game game = new Game();
      _rooms = game.Rooms;
    }

    [Test]
    public void CanCrateRoomAndPlayerWillHere()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(player, 10);

      Assert.AreEqual(true, room.Players.Contains(player));
    }

    [Test]
    public void CanCrateRoomAndAddTwoPlayers()
    {
      var player1 = new Player("Ioan");
      IRoom room = CreateRoom(player1, 10);
      
      var player2 = new Player("Gilbert");
      _rooms.Connect(player2, room);
      
      Assert.AreEqual(true, room.Players.Contains(player2));
    }
    
    [Test]
    public void AddingPlayerInRoomTwiceTrowsException()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(player, 10);
      
      Assert.Throws<InvalidOperationException>(() => _rooms.Connect(player, room));
    }
    
    [Test]
    public void MakingPlayerAsReadyIncreasedReadyCountInRoom()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(player, 10);
      room.MakeReady(player);
      
      Assert.AreEqual(1, room.ReadyPlayersCount);
    }
    

    private IRoom CreateRoom(Player player, int maxPlayers)
    {
      IRoom room = _rooms.CreateRoomAndConnect(player, maxPlayers);
      return room;
    }

  }
}