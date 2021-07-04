using System;

namespace Tasks
{
  public struct Cell
  {
    public Cell(Good good, int count = 0)
    {
      if(count < 0)
        throw new ArgumentOutOfRangeException(nameof(count));
        
      Good = good;
      Count = count;
    }

    public Good Good { get; }
    public int Count { get; }

    public Cell AddItem(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));

      return new Cell(Good, Count + count);
    }
    
    public Cell RemoveItem(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));
      
      if (count > Count)
        throw new InvalidOperationException("Can't remove more than contain cell");

      return new Cell(Good, Count - count);
    }
  }
}