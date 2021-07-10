using System;
using System.Linq;
using NUnit.Framework;
using Source;

namespace GameLobbyTest
{
  public class Tests
  {

    private Lobby _lobby;

    [SetUp]
    public void Setup()
    {
      Game game = new Game();
      _lobby = game.Lobby;
    }

    [Test]
    public void CanCrateRoomAndPlayerWillHere()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(10);
      _lobby.Connect(player, room);

      Assert.AreEqual(true, room.Players.Contains(player));
    }

    [Test]
    public void CanCrateRoomAndAddTwoPlayers()
    {
      var player1 = new Player("Ioan");
      IRoom room = CreateRoom(10);
      _lobby.Connect(player1, room);
      
      var player2 = new Player("Gilbert");
      _lobby.Connect(player2, room);
      
      Assert.AreEqual(true, room.Players.Contains(player2));
    }
    
    [Test]
    public void AddingPlayerInRoomTwiceTrowsException()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(10);
      _lobby.Connect(player, room);
      
      Assert.Throws<InvalidOperationException>(() => _lobby.Connect(player, room));
    }
    
    [Test]
    public void MakingPlayerAsReadyIncreasedReadyCountInRoom()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(10);
      _lobby.Connect(player, room);
      room.MakeReady(player);
      
      Assert.AreEqual(1, room.ReadyPlayersCount);
    }
    
    [Test]
    public void MakingPlayerWitchAbsentInRoomAsReadyTrowsException()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(10);

      Assert.Throws<InvalidOperationException>(() => room.MakeReady(player));
    }
    
    private IRoom CreateRoom(int maxPlayers)
    {
      IRoom room = _lobby.CreateRoom(maxPlayers);
      return room;
    }


  }
}