using System;

namespace Source
{
  public interface IConnectionId
  {
    int Id { get; }
  }

  public interface IConnection : IConnectionId
  {
    Player Player { get; }
    Room Room { get; }
    bool IsPlayerReady { get; }
  }
}