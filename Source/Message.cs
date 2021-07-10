using System;

public class Message
{
  public string Sender { get; }
  public string Text { get; }

  public Message(string sender, string text)
  {
    if (!IsValidString(sender))
      throw new InvalidOperationException($"argument {nameof(sender)} is not valid");
    
    if (!IsValidString(text))
      throw new InvalidOperationException($"argument {nameof(text)} is empty or white space");
    
    Sender = sender;
    Text = text;
  }

  private static bool IsValidString(string value) => 
    !string.IsNullOrEmpty(value.Replace(" ", string.Empty));
}