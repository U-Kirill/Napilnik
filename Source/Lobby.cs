using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Source
{
    public class Lobby : ILobby
    {
        private State _state;

        private readonly List<LobbyConnection> _connections = new List<LobbyConnection>();
        private readonly Chat _chat = new Chat();

        public Lobby(int maxPlayers)
        {
            MaxPlayers = maxPlayers;
            _state = new WaitingState(this);
        }

        public int MaxPlayers { get; }

        public int ReadyPlayersCount => ReadyPlayers.Count();

        private IEnumerable<LobbyConnection> ReadyPlayers => _connections.Where(x => x.IsPlayerReady);

        public void Connect(LobbyConnection connection)
        {
            ValidateConnection(connection);
            _connections.Add(connection);
        }

        public bool HasFreeSlots() =>
            ReadyPlayersCount < MaxPlayers;

        public void MakeReady(Player player)
        {
            ValidatePlayer(player);

            if (!HasFreeSlots())
                throw new InvalidOperationException();

            LobbyConnection connection = _connections.Find(x => x.Player == player);
            connection.MakeReady();
            TryChangeState();
        }

        public bool HasPlayer(IPlayer player) => 
            _connections.Any(x => x.Player == player);

        public bool CanUseChat(IPlayer player)
        {
            ValidatePlayer(player);
            return _state.CanUseChat(player);
        }

        public void PrintMessage(string message, IPlayer player)
        {
            ValidatePlayer(player);

            if (!CanUseChat(player))
                throw new InvalidOperationException();

            _chat.Add(message, player);
            _state.Notify();
        }

        public IEnumerable<Message> LoadMessage(int lastMessageId, IPlayer player)
        {
            ValidatePlayer(player);

            if (!CanUseChat(player))
                throw new InvalidOperationException();

            return _chat.LoadSince(lastMessageId);
        }

        private void TryChangeState()
        {
            if (!HasFreeSlots())
                ChangeState();
        }

        private void ChangeState() => 
            _state = new GameState(this, ReadyPlayers);

        private void ValidatePlayer(IPlayer player)
        {
            if (!HasPlayer(player))
                throw new InvalidOperationException();
        }

        private void ValidateConnection(LobbyConnection connection)
        {
            if (connection == null)
                throw new ArgumentException();

            if (!HasFreeSlots())
                throw new InvalidOperationException();
        }

        public abstract class State
        {
            private Lobby _lobby;

            protected State(Lobby lobby)
            {
                _lobby = lobby;
            }

            public void Notify()
            {
                IEnumerable<LobbyConnection> lobbyConnections = GetConnectionsForNotify(_lobby._connections);

                foreach (LobbyConnection lobbyConnection in lobbyConnections)
                    lobbyConnection.OnChatUpdate();
            }

            public bool CanUseChat(IPlayer player)
            {
                IEnumerable<LobbyConnection> lobbyConnections = GetConnectionsForNotify(_lobby._connections);
                return lobbyConnections.Any(x => x.Player == player);
            }

            protected abstract IEnumerable<LobbyConnection> GetConnectionsForNotify(IEnumerable<LobbyConnection> allConnections);
        }
    }

    internal class GameState : Lobby.State
    {
        private readonly IEnumerable<LobbyConnection> _readyPlayers;

        public GameState(Lobby lobby, IEnumerable<LobbyConnection> readyPlayers) : base(lobby) => 
            _readyPlayers = readyPlayers;

        protected override IEnumerable<LobbyConnection> GetConnectionsForNotify(IEnumerable<LobbyConnection> allConnections) => 
            _readyPlayers;
    }

    internal class WaitingState : Lobby.State
    {
        public WaitingState(Lobby lobby) : base(lobby)
        {
        }

        protected override IEnumerable<LobbyConnection> GetConnectionsForNotify(IEnumerable<LobbyConnection> allConnections) => 
            allConnections;
    }
}