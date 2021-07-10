using System;

namespace Source
{
  public interface IConnection
  {
    Player Player { get; }

    bool IsPlayerReady { get; }
    IRoom Room { get; }

  }
}