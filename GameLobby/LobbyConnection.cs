using System;
using GameLobby.Commands.Abstractions;
using GameLobby.Rooms;

namespace GameLobby
{
    public class LobbyConnection : ILobbyConnection
    {
        private readonly Lobby _lobby;
        private readonly Player _player;

        public LobbyConnection(Player player, Lobby lobby)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _lobby = lobby ?? throw new ArgumentNullException(nameof(lobby));
        }

        public event Action ChatUpdated;

        public bool IsPlayerReady { get; private set; }

        public IPlayer Player => _player;

        public void ApplyCommand(ILobbyCommand command) =>
            command.Execute(_player, _lobby);

        public void OnChatUpdate() =>
            ChatUpdated?.Invoke();

        public void MakeReady()
        {
            if (IsPlayerReady)
                throw new InvalidOperationException("Player already ready");

            IsPlayerReady = true;
        }
    }
}