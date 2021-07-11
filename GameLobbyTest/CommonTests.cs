using System;
using System.Linq;
using NUnit.Framework;
using Source;

namespace GameLobbyTest
{
  public class CommonTests
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
      IConnection connection = CreateRoomAndConnect(player, 10);
      
      Assert.AreEqual(true, connection.Room.Connections.Any(x =>  x.Player == player));
    }

    [Test]
    public void CanCrateRoomAndAddTwoPlayers()
    {
      var player1 = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player1, 10);
      
      var player2 = new Player("Gilbert");
      _lobby.Connect(player2, connection.Room);
      
      Assert.AreEqual(true, connection.Room.Connections.Any(x =>  x.Player == player2));
    }

    [Test]
    public void AddingPlayerInRoomTwiceTrowsException()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 10);
      
      Assert.Throws<InvalidOperationException>(() => _lobby.Connect(player, connection.Room));
    }
    
    [Test]
    public void AddingPlayerInDifferentRoomsTrowsException()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 10);
      
      Assert.Throws<InvalidOperationException>(() => CreateRoomAndConnect(player, 10));
    }

    [Test]
    public void MakingPlayerAsReadyIncreasedReadyCountInRoom()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 10);
      connection.Room.MakeReady(player);
      
      Assert.AreEqual(1, connection.Room.Connections.Count(x => x.IsPlayerReady));
    }

    [Test]
    public void MakingPlayerWitchAbsentInRoomAsReadyTrowsException()
    {
      var player = new Player("Ioan");
      IRoom room = CreateRoom(10);

      Assert.Throws<InvalidOperationException>(() => room.MakeReady(player));
    }
    
    [Test]
    public void CanReadSentMessage()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 10);
      
      var sentText = "Hello Room!";
      Message receivedMessage = SendAndReceiveMessage(connection, sentText);

      Assert.AreEqual(sentText, receivedMessage.Text);
    }

    [Test]
    public void AreEqualSenderAndSenderInReceivedMessage()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 10);

      Message receivedMessage = SendAndReceiveMessage(connection, "Hello Room!");

      Assert.AreEqual(player, receivedMessage.Sender);
    }

    [Test]
    public void CanWriteInGameSate()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 1);

      _lobby.Connect(new Player("Gilbert"), connection.Room);
      
      Message receivedMessage = SendAndReceiveMessage(connection, "Hello Room!");

      Assert.AreEqual(player, receivedMessage.Sender);
    }
    
    [Test]
    public void WriteInGameSateIfPlayerWarUnreadyInStartGameTrowsException()
    {
      var player = new Player("Ioan");
      IConnection connection1 = CreateRoomAndConnect(player, 1);
      IConnection connection2 = _lobby.Connect(new Player("Gilbert"), connection1.Room);

      connection1.Room.MakeReady(player);
      Assert.Throws<InvalidOperationException>(() => SendAndReceiveMessage(connection2, "Hello Room!"));
    }
    
    [Test]
    public void CanWriteIfRoomHasMoreConnectionsThanPossibleReadyConnections()
    {
      var player = new Player("Ioan");
      IConnection connection1 = CreateRoomAndConnect(player, 1);
      IConnection connection2 = _lobby.Connect(new Player("Gilbert"), connection1.Room);

      Message receivedMessage = SendAndReceiveMessage(connection2, "Hello Room!");
    
      Assert.AreEqual(connection2.Player, receivedMessage.Sender);
    }
    
    [Test]
    public void MakeReadyIfPlayerNotActiveTrowsException()
    {
      var player = new Player("Ioan");
      IConnection connection1 = CreateRoomAndConnect(player, 1);
      IConnection connection2 = _lobby.Connect(new Player("Gilbert"), connection1.Room);

      connection1.Room.MakeReady(player);
      Assert.Throws<InvalidOperationException>(() => connection2.Room.MakeReady(player));
    }
    
    private Message SendAndReceiveMessage(IConnection connection, string text)
    {
      Message recivedMessage = null;
      connection.MessageReceived += message => recivedMessage = message;

      connection.Room.SendMessage(connection.Player, text);
      return recivedMessage;
    }

    private IConnection CreateRoomAndConnect(Player player, int maxPlayers)
    {
      IRoom room = CreateRoom(maxPlayers);
      IConnection connection = _lobby.Connect(player, room);
      return connection;
    }

    private IRoom CreateRoom(int maxPlayers)
    {
      IRoom room = _lobby.CreateRoom(maxPlayers);
      return room;
    }
  }
}