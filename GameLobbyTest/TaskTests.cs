using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Source;

namespace GameLobbyTest
{
  public class TaskTests
  {
    private Lobby _lobby;

    [SetUp]
    public void Setup()
    {
      Game game = new Game();
      _lobby = game.Lobby;
    }
    
    [Test]
    public void CanCreateRoom()
    {
      _lobby.CreateRoom(10);
      Assert.AreEqual(1, _lobby.Rooms.Count);
    }
    
    [Test]
    public void CanCreateRoomAndConnect()
    {
      IRoom room = _lobby.CreateRoom(10);
      Player player = new Player("Tester");
      
      _lobby.Connect(player, room);

      Assert.AreEqual(player, room.Connections[0].Player);
    }
    
    [Test]
    public void PlayerIsNotReadyAfterConnection()
    {
      IRoom room = _lobby.CreateRoom(10);
      Player player = new Player("Tester");

      IConnection connection = _lobby.Connect(player, room);

      Assert.AreEqual(false, connection.IsPlayerReady);
    }
    
    [Test]
    public void CanMakePlayerReady()
    {
      IRoom room = _lobby.CreateRoom(10);
      Player player = new Player("Tester");

      IConnection connection = _lobby.Connect(player, room);

      connection.Room.MakeReady(connection.Player);
      
      Assert.AreEqual(true, connection.IsPlayerReady);
    }
    
    [Test]
    public void ConnectToFullReadyPlayerRoomThrowsException()
    {
      IRoom room = _lobby.CreateRoom(1);
      Player player = new Player("Tester");

      IConnection connection = _lobby.Connect(player, room);
      connection.Room.MakeReady(connection.Player);

      Assert.Throws<InvalidOperationException>(() => _lobby.Connect(new Player("Violate"), room));
    }
    
    [Test]
    public void CanConnectToFullButNotReadyPlayerRoom()
    {
      IRoom room = _lobby.CreateRoom(1);
      
      var player1 = new Player("Tester");
      var player2 = new Player("Violate");

      IConnection connection1 = _lobby.Connect(player1, room);
      IConnection connection2 = _lobby.Connect(player2, room);

      Assert.AreEqual(player2, room.Connections[1].Player);
    }
    
    [Test]
    public void CanSendMessageAndReadIt()
    {
      IRoom room = _lobby.CreateRoom(1);
      
      var player1 = new Player("Tester");
      IConnection connection1 = _lobby.Connect(player1, room);

      Message recieavedMessage = null;
      connection1.MessageReceived += message => recieavedMessage = message;
      
      string sentMessage = "Hello Room!";
      room.SendMessage(player1, sentMessage);
      
      Assert.AreEqual(sentMessage,recieavedMessage.Text);
    }
    
    [Test]
    public void SendMessageInGameStateRoomFromNotReadyPlayerThrowsException()
    {
      IRoom room = _lobby.CreateRoom(1);
      
      var player1 = new Player("Tester 1");
      var player2 = new Player("Tester 2");
      IConnection connection1 = _lobby.Connect(player1, room);
      IConnection connection2 = _lobby.Connect(player2, room);

      room.MakeReady(player1);
      
      Message recieavedMessage = null;
      connection1.MessageReceived += message => recieavedMessage = message;
      
      Assert.Throws<InvalidOperationException>(() => room.SendMessage(player2, "Hello Room!"));
    }
    
    [Test]
    public void CanSendAndReadMessageInGameStateRoomFromReadyPlayer()
    {
      IRoom room = _lobby.CreateRoom(1);
      
      var player1 = new Player("Tester 1");
      var player2 = new Player("Tester 2");
      IConnection connection1 = _lobby.Connect(player1, room);
      IConnection connection2 = _lobby.Connect(player2, room);

      room.MakeReady(player1);
      
      Message recieavedMessage = null;
      connection1.MessageReceived += message => recieavedMessage = message;
      
      string sentMessage = "Hello Room!";
      room.SendMessage(player1, sentMessage);
      
      Assert.AreEqual(sentMessage,recieavedMessage.Text);
    }
    
    
    [Test]
    public void CanNotReadMessageInGameStateRoomFromNotReadyReadyPlayer()
    {
      IRoom room = _lobby.CreateRoom(1);
      
      var player1 = new Player("Tester 1");
      var player2 = new Player("Tester 2");
      IConnection connection1 = _lobby.Connect(player1, room);
      IConnection connection2 = _lobby.Connect(player2, room);

      room.MakeReady(player1);
      
      Message receivedMessage = null;
      connection2.MessageReceived += message => receivedMessage = message;
      
      string sentMessage = "Hello Room!";
      room.SendMessage(player1, sentMessage);
      
      Assert.AreEqual(null,receivedMessage);
    }
  }
}