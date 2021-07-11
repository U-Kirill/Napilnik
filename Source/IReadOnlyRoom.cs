using System.Collections.Generic;

namespace Source
{
  public interface IReadOnlyRoom
  {
    IReadOnlyList<IPlayerConnection> Connections { get; }
    int MaxPlayers { get; }

  }
}