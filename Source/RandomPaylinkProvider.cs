
using System;
using System.Linq;

namespace Tasks
{
  public class RandomPaylinkProvider : IPaylinkProvider
  {
    private readonly int _linkLenght;
    
    private Random _random = new Random();
    
    
    public RandomPaylinkProvider(int linkLenght)
    {
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