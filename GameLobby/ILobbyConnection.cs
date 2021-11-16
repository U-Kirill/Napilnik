using System;
using GameLobby.Commands.Abstractions;

namespace GameLobby
{
    public interface ILobbyConnection
    {
        event Action ChatUpdated;

        bool IsPlayerReady { get; }

        void ApplyCommand(ILobbyCommand command);
    }
}