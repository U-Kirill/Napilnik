using System;
using System.Collections.Generic;
using Source;

internal class Chat
{
  private readonly  List<Message> _messages = new List<Message>();

  public event Action<Message> MessageRecived;
  
  public void Write(Player sender, string text)
  {
    var message = new Message(sender, text);
    _messages.Add(message);

    MessageRecived?.Invoke(message);
  }
}