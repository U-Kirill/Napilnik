namespace Source
{
    public interface IReturnableLobbyCommand<T> : ILobbyCommand
    {
        T Result { get; }
    }
}