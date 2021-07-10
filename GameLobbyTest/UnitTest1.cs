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

      var sentText = "Hello Room!";
      Message receivedMessage = SendAndReceiveMessage(connection, sentText);

      Assert.AreEqual(player, receivedMessage.Sender);
    }

    [Test]
    public void IfReachNesseseryReadyPlayersCountRoomeGoesToGameState()
    {
      var player = new Player("Ioan");
      IConnection connection = CreateRoomAndConnect(player, 10);

      var sentText = "Hello Room!";
      Message receivedMessage = SendAndReceiveMessage(connection, sentText);

      Assert.AreEqual(player, receivedMessage.Sender);
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