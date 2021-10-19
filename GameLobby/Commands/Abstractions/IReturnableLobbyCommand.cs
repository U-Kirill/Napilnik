namespace GameLobby.Commands.Abstractions
{
    public interface IReturnableLobbyCommand<T> : ILobbyCommand
    {
        T Result { get; }
    }
}