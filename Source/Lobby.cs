using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Source
{
    public class Lobby : ILobby
    {
    
        //private readonly List<IPlayer> Players = new List<IPlayer>();
        private readonly List<LobbyConnection> _connections = new List<LobbyConnection>();
        private State _state;

        public Lobby(int maxPlayers)
        {
            MaxPlayers = maxPlayers;
            _state = new WaitingState(this);
        }

        public int MaxPlayers { get; }
        public int ReadyPlayersCount => ReadyPlayers.Count();
        public IEnumerable<LobbyConnection> ReadyPlayers => _connections.Where(x => x.IsPlayerReady);

        public void Connect(LobbyConnection connection)
        {
            ValidateConnection(connection);
            _connections.Add(connection);
            TryChangeState();
        }

        private void TryChangeState()
        {
            if (!CanConnect())
                _state = new GameState(this, ReadyPlayers);
        }

        private void ValidateConnection(LobbyConnection connection)
        {
            if (connection == null)
                throw new ArgumentException();

            if (!CanConnect())
                throw new InvalidOperationException();
        }

        public bool CanConnect() => 
            ReadyPlayersCount < MaxPlayers;
        
        public abstract class State
        {
            private Lobby _lobby;

            protected State(Lobby lobby)
            {
                _lobby = lobby;
            }

            public void Notify()
            {
                IEnumerable<LobbyConnection> lobbyConnections = GetConnectionsForNotify(_lobby.ReadyPlayers);

                foreach (LobbyConnection lobbyConnection in lobbyConnections)
                    lobbyConnection.OnChatUpdate();
            }

            public bool CanSpeak(IPlayer player)
            {
                IEnumerable<LobbyConnection> lobbyConnections = GetConnectionsForNotify(_lobby.ReadyPlayers);
                return lobbyConnections.Any(x => x.Player == player);
            }

            protected abstract IEnumerable<LobbyConnection> GetConnectionsForNotify(IEnumerable<LobbyConnection> lobbyReadyPlayers);
        }

    }

    internal class GameState : Lobby.State
    {
        private readonly IEnumerable<LobbyConnection> _readyPlayers;

        public GameState(Lobby lobby, IEnumerable<LobbyConnection> readyPlayers) : base(lobby) => 
            _readyPlayers = readyPlayers;

        protected override IEnumerable<LobbyConnection> GetConnectionsForNotify(IEnumerable<LobbyConnection> lobbyReadyPlayers) => 
            _readyPlayers;
    }

    internal class WaitingState : Lobby.State
    {
        public WaitingState(Lobby lobby) : base(lobby)
        {
        }

        protected override IEnumerable<LobbyConnection> GetConnectionsForNotify(IEnumerable<LobbyConnection> lobbyReadyPlayers) => 
            lobbyReadyPlayers;
    }
}