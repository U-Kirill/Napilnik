using System;
using Source;

public class Message
{
  public Message(Player sender, string text)
  {
    sender = sender ?? throw new NullReferenceException(nameof(sender));
    
    if (!IsValidString(text))
      throw new InvalidOperationException($"argument {nameof(text)} is empty or white space");
    
    Sender = sender;
    Text = text;
  }

  public string Text { get; }
  public Player Sender { get; }

  private bool IsValidString(string value) => 
    !string.IsNullOrEmpty(value.Replace(" ", string.Empty));
}