using System;

namespace Napilnik.Encapsulation
{
  public class Good
  {
    public string Name { get; }

    public Good(string name)
    {
      if (name == null)
        throw new NullReferenceException(nameof(name));
      
      Name = name;
    }
  }
}