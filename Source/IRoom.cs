namespace Source
{
    public interface IRoom : IReadOnlyRoom
    {
        void MakeReady(Player player);
        void SendMessage(Player sender, string message);
        bool IsActivePlayer(Player sender);
    }
}