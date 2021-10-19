using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class Game
    {
        private List<Lobby> _lobbies = new List<Lobby>();

        public IReadOnlyList<ILobby> Lobbies => _lobbies;

        public void Connect(Player player, ILobby lobby)
        {
            if (!HasLobby(lobby))
                throw new InvalidOperationException();

            if (IsAnyLobbiesHas(player))
                throw new InvalidOperationException();

            Lobby targetLobby = GetLobby(lobby);
            LobbyConnection connection = new LobbyConnection(player, targetLobby);
            targetLobby.Connect(connection);
            player.Connect(connection);
        }

        public ILobby Create(int maxPlayers)
        {
            var lobby = new Lobby(maxPlayers);
            _lobbies.Add(lobby);

            return lobby;
        }

        private bool IsAnyLobbiesHas(Player player) => 
            _lobbies.Any(x => x.HasPlayer(player));

        private bool HasLobby(ILobby lobby) => 
            GetLobby(lobby) != null;

        private Lobby GetLobby(ILobby lobby) => 
            _lobbies.Find(x => x == lobby);
    }
}