using System;
using System.Collections.Generic;

internal class Chat
{

  private readonly  List<Message> _messages = new List<Message>();

  public event Action<Message> Written;
  
  public void Write(string senderName, string text)
  {
    var message = new Message(senderName, text);
    _messages.Add(message);

    Written?.Invoke(message);
  }

  public IReadOnlyList<Message> Messages => _messages;

}