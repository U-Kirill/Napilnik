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
      IConnection connection = CreateRoomAndConnect(new Player("Tester"), 10);

      Assert.AreEqual(connection.Player, connection.Room.Connections[0].Player);
    }

    [Test]
    public void PlayerIsNotReadyAfterConnection()
    {
      IConnection connection = CreateRoomAndConnect(new Player("Tester"), 10);

      Assert.AreEqual(false, connection.IsPlayerReady);
    }

    [Test]
    public void CanMakePlayerReady()
    {
      IConnection connection = CreateRoomAndConnect(new Player("Tester"), 10);
      connection.Room.MakeReady(connection.Player);
      
      Assert.AreEqual(true, connection.IsPlayerReady);
    }

    [Test]
    public void ConnectToFullReadyPlayerRoomThrowsException()
    {
      IConnection connection = CreateRoomAndConnect(new Player("Tester"), 1);
      connection.Room.MakeReady(connection.Player);

      Assert.Throws<InvalidOperationException>(() => _lobby.Connect(new Player("Violate"), connection.Room));
    }
    
    [Test]
    public void MakeReadyInFullReadyPlayerRoomThrowsException()
    {
      IConnection connection = CreateRoomAndConnect(new Player("Tester"), 1);
      connection.Room.MakeReady(connection.Player);

      Assert.Throws<InvalidOperationException>(() => connection.Room.MakeReady(new Player("Violate")));
    }

    [Test]
    public void CanConnectToFullButNotReadyPlayerRoom()
    {
      IConnection connection = CreateRoomAndConnect(new Player("Tester"), 1);
      
      var player2 = new Player("Violate");
      _lobby.Connect(player2, connection.Room);

      Assert.AreEqual(player2, connection.Room.Connections[1].Player);
    }

    [Test]
    public void CanSendMessageAndReadIt()
    {
      var player = new Player("Tester");
      IConnection connection = CreateRoomAndConnect(player, 10);
      
      string sentMessage = "Hello Room!";
      Message receivedMessage = SendAndReceiveMessage(connection, connection, sentMessage);

      Assert.AreEqual(sentMessage,receivedMessage.Text);
    }

    [Test]
    public void SendMessageInGameStateRoomFromNotReadyPlayerThrowsException()
    {
      IConnection connection1 = CreateRoomAndConnect(new Player("Tester 1"), 1);
      IConnection connection2 = _lobby.Connect(new Player("Tester 2"), connection1.Room);

      connection1.Room.MakeReady(connection1.Player);

      Assert.Throws<InvalidOperationException>(() => SendAndReceiveMessage(connection2, connection1, "Hello Room!"));
    }

    [Test]
    public void CanSendAndReadMessageInGameStateRoomFromReadyPlayer()
    {
      IConnection connection1 = CreateRoomAndConnect(new Player("Tester 1"), 1);
      IConnection connection2 = _lobby.Connect(new Player("Tester 2"), connection1.Room);

      connection1.Room.MakeReady(connection1.Player);

      string sentMessage = "Hello Room!";
      Message receivedMessage = SendAndReceiveMessage(connection1, connection1, sentMessage);

      Assert.AreEqual(sentMessage,receivedMessage.Text);
    }


    [Test]
    public void CanNotReadMessageInGameStateRoomFromNotReadyReadyPlayer()
    {
      IConnection connection1 = CreateRoomAndConnect(new Player("Tester 1"), 1);
      IConnection connection2 = _lobby.Connect(new Player("Tester 2"), connection1.Room);

      connection1.Room.MakeReady(connection1.Player);

      string sentMessage = "Hello Room!";
      Message receivedMessage = SendAndReceiveMessage(connection1, connection2, sentMessage);
      
      Assert.AreEqual(null,receivedMessage);
    }

    private IConnection CreateRoomAndConnect(Player player, int maxPlayers)
    {
      IRoom room = _lobby.CreateRoom(maxPlayers);
      IConnection connection = _lobby.Connect(player, room);
      return connection;
    }

    private Message SendAndReceiveMessage(IConnection sender, IConnection receiver, string message)
    {
      Message receivedMessage = null;
      receiver.MessageReceived += message => receivedMessage = message;

      sender.Room.SendMessage(sender.Player, message);
      return receivedMessage;
    }

  }
}