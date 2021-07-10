using System.Collections.Generic;

namespace Source
{
  public interface IRoom
  {
    void MakeReady(Player player);
    void SendMessage(Player sender, string message);
    IReadOnlyList<IPlayerConnection> Connections { get; }
    int MaxPlayers { get; }

  }
}