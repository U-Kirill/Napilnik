using System.Collections.Generic;

namespace GameLobby.Rooms
{
    internal class WaitingState : Lobby.State
    {
        public WaitingState(Lobby lobby) : base(lobby)
        {
        }

        protected override IEnumerable<LobbyConnection> GetActiveConnections(IEnumerable<LobbyConnection> allConnections) =>
            allConnections;
    }
}