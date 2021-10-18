using System.Collections.Generic;

namespace Source
{
    public class Game
    {
        private List<Lobby> _lobbies = new List<Lobby>();
        private int _maxPlayers = 2;

        public IReadOnlyList<ILobby> Lobbies => _lobbies;

        public void Connect(Player player, ILobby lobby)
        {
            Lobby targetLobby = GetLobby(lobby);
            LobbyConnection connection = new LobbyConnection(player, targetLobby);
            targetLobby.Connect(connection);
            player.Connect(connection);
        }

        public void Create()
        {
            _lobbies.Add(new Lobby(_maxPlayers));
        }
    
        private Lobby GetLobby(ILobby lobby) => 
            _lobbies.Find(x => x == lobby);
    }
}