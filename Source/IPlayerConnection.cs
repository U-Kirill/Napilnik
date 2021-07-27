namespace Source
{
    public interface IPlayerConnection
    {
        Player Player { get; }
        bool IsPlayerReady { get; }
    }
}