using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class Game
    {
        private List<Lobby> _lobbies = new List<Lobby>();
        private int _maxPlayers = 2;

        public IReadOnlyList<ILobby> Lobbies => _lobbies;

        public void Connect(Player player, ILobby lobby)
        {
            if (IsAnyLobbiesHas(player))
                throw new InvalidOperationException();
            
            Lobby targetLobby = GetLobby(lobby);
            LobbyConnection connection = new LobbyConnection(player, targetLobby);
            targetLobby.Connect(connection);
            player.Connect(connection);
        }

        private bool IsAnyLobbiesHas(Player player)
        {
            return _lobbies.Any(x => x.HasPlayer(player));
        }

        public ILobby Create()
        {
            var lobby = new Lobby(_maxPlayers);
            _lobbies.Add(lobby);
            return lobby;
        }
    
        private Lobby GetLobby(ILobby lobby) => 
            _lobbies.Find(x => x == lobby);
    }
}