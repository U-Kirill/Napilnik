using System;
using Source;

public class Message
{
  public Player Sender { get; }
  public string Text { get; }

  public Message(Player sender, string text)
  {
    sender = sender ?? throw new NullReferenceException(nameof(sender));
    
    if (!IsValidString(text))
      throw new InvalidOperationException($"argument {nameof(text)} is empty or white space");
    
    Sender = sender;
    Text = text;
  }

  private static bool IsValidString(string value) => 
    !string.IsNullOrEmpty(value.Replace(" ", string.Empty));
}