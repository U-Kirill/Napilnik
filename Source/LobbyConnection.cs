using System;

namespace Source
{
    public interface ILobbyConnection
    {
        bool IsPlayerReady { get; set; }
        event Action ChatUpdated;
        void PrintMessage(string message);
        void ReadAllMessages();
        void ReadLastMessage();
        bool CanUseChat();
        void MakeReady();
    }

    public class LobbyConnection : ILobbyConnection
    {
        private readonly Lobby Lobby;

        public bool IsPlayerReady { get; set; }

        public LobbyConnection(Player player, Lobby lobby)
        {
            Player = player;
            Lobby = lobby;
        }

        public event Action ChatUpdated;

        public IPlayer Player { get; }
        
        public void PrintMessage(string message)
        {
            Lobby.PrintMessage(Player, message);
        }

        public void ReadAllMessages()
        {
        
        }

        public void ReadLastMessage()
        {
      
        }
    
        public bool CanUseChat()
        {
            return Lobby.CanUseChat(Player);
        }

        public void MakeReady()
        {
            IsPlayerReady = true;
        }

        public void OnChatUpdate()
        {
            if (CanUseChat())
                ChatUpdated?.Invoke();
        }
    }
}