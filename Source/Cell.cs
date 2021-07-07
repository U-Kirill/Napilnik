using System;

namespace Napilnik.Encapsulation
{
  public struct Cell
  {
    public Cell(Good good, int count = 0)
    {
      if (good == null)
        throw new NullReferenceException(nameof(good));

      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof(count));

      Good = good;
      Count = count;
    }

    public Good Good { get; }
    public int Count { get; }

    public Cell Add(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));

      return new Cell(Good, Count + count);
    }
    
    public Cell Remove(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));
      
      if (count > Count)
        throw new InvalidOperationException("Can't remove more than contain cell");

      return new Cell(Good, Count - count);
    }

    public override string ToString() => 
      $"{nameof(Good)}: {Good}, {nameof(Count)}: {Count}";
  }
}