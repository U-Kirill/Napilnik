using System.Collections.Generic;

namespace Source
{
  public interface IRoom
  {
    void MakeReady(Player player);
    void SendMessage(Player sender, string message);
    string GetAllMessages(Player reader);
    IEnumerable<Player> Players { get; }
    int ReadyPlayersCount { get; }
    int MaxPlayers { get; }

  }
}