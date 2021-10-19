using System.Collections.Generic;

namespace GameLobby.Rooms
{
    internal class GameState : Lobby.State
    {
        private readonly IEnumerable<LobbyConnection> _readyPlayers;

        public GameState(Lobby lobby, IEnumerable<LobbyConnection> readyPlayers) : base(lobby)
        {
            _readyPlayers = readyPlayers;
        }

        protected override IEnumerable<LobbyConnection> GetActiveConnections(IEnumerable<LobbyConnection> allConnections)
        {
            return _readyPlayers;
        }
    }
}