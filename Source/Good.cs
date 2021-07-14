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

      if (!IsValidName(name))
        throw new ArgumentOutOfRangeException($"{nameof(name)} is null or emply. Value: {name}");

      Name = name;
    }

    private bool IsValidName(string name) =>
      !string.IsNullOrEmpty(name?.Replace(" ", string.Empty));

  }
}