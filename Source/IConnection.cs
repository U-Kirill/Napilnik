using System;

namespace Source
{
    public interface IConnection : IPlayerConnection
    {
        event Action<Message> MessageReceived;
        IRoom Room { get; }
    }
}