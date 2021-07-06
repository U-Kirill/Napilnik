
using System;
using System.Linq;

namespace Napilnik.Encapsulation
{
  public class RandomPaylinkProvider : IPaylinkProvider
  {
    private readonly int _linkLenght;
    private readonly Random _random = new Random();
    
    public RandomPaylinkProvider(int linkLenght)
    {
      if (linkLenght < 1)
        throw new ArgumentOutOfRangeException(nameof(linkLenght));
      
      _linkLenght = linkLenght;
    }
    
    public string GetLink() =>
      GetRandomString();

    private string GetRandomString()
    {
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      
      return new string(Enumerable.Repeat(chars, _linkLenght)
        .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
  }
}