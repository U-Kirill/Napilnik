namespace Source
{
    public class PrintMessageCommand : ILobbyCommand
    {
        private string _message;

        public PrintMessageCommand(string message)
        {
            _message = message;
        }

        public void Execute(Player player, Lobby lobby) => 
            lobby.PrintMessage(_message, player);
    }
}