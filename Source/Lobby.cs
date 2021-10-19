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
        private Chat _chat = new Chat();

        public Lobby(int maxPlayers)
        {
            MaxPlayers = maxPlayers;
            _state = new WaitingState(this);
        }

        public int MaxPlayers { get; }
        
        public IEnumerable<LobbyConnection> ReadyPlayers => _connections.Where(x => x.IsPlayerReady);
        
        public int ReadyPlayersCount => ReadyPlayers.Count();

        public void Connect(LobbyConnection connection)
        {
            ValidateConnection(connection);
            _connections.Add(connection);
            connection.StatusChanged += TryChangeState;
        }

        public bool CanConnect() => 
            ReadyPlayersCount < MaxPlayers;

        public bool CanUseChat(IPlayer player) => 
            _state.CanUseChat(player);

        public void PrintMessage(string message, IPlayer player)
        {
            if (CanUseChat(player))
                _chat.Add(message, player);
            
            _state.Notify();
        }

        public IEnumerable<Message> LoadMessage(int lastMessageId, IPlayer player)
        {
            IEnumerable<Message> messages = Array.Empty<Message>();
            
            if (CanUseChat(player))
                messages = _chat.LoadSince(lastMessageId);
            
            return messages;
        }

        public bool HasPlayer(IPlayer player) => 
            _connections.Any(x => x.Player == player);

        private void TryChangeState()
        {
            if (!CanConnect())
                ChangeState();
        }
        
        
        private void ChangeState() => 
            _state = new GameState(this, ReadyPlayers);

        private void ValidateConnection(LobbyConnection connection)
        {
            if (connection == null)
                throw new ArgumentException();

            if (!CanConnect())
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