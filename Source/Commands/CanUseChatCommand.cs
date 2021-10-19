namespace Source
{
    public class CanUseChatCommand : IReturnableLobbyCommand<bool>
    {
        public bool Result { get; private set; }

        public void Execute(Player player, Lobby lobby)
        {
            Result = lobby.CanUseChat(player);
        }
    }
}