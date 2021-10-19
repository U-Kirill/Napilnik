using System.Collections.Generic;

namespace Source
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