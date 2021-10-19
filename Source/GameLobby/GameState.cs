using System.Collections.Generic;

namespace Source
{
    internal class GameState : Lobby.State
    {
        private readonly IEnumerable<LobbyConnection> _readyPlayers;

        public GameState(Lobby lobby, IEnumerable<LobbyConnection> readyPlayers) : base(lobby) => 
            _readyPlayers = readyPlayers;

        protected override IEnumerable<LobbyConnection> GetActiveConnections(IEnumerable<LobbyConnection> allConnections) => 
            _readyPlayers;
    }
}