using System;
using System.Linq;

namespace Source
{
    public interface ILobbyConnection
    {
        event Action ChatUpdated;

        bool IsPlayerReady { get; }

        void ApplyCommand(ILobbyCommand command);
    }

    public class LobbyConnection : ILobbyConnection
    {
        private readonly Lobby _lobby;
        private readonly Player _player;

        public LobbyConnection(Player player, Lobby lobby)
        {
            _player = player ?? throw new ArgumentNullException();
            _lobby = lobby ?? throw new ArgumentNullException();
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
                throw new InvalidOperationException();

            IsPlayerReady = true;
        }
    }
}