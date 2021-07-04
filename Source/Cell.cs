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

    public Cell IncreaseQuantity(int quantity)
    {
      if (quantity <= 0)
        throw new ArgumentOutOfRangeException(nameof(quantity));

      return new Cell(Good, Count + quantity);
    }
    
    public Cell ReduceQuantity(int quantity)
    {
      if (quantity <= 0)
        throw new ArgumentOutOfRangeException(nameof(quantity));
      
      if (quantity > Count)
        throw new InvalidOperationException("Can't remove more than contain cell");

      return new Cell(Good, Count - quantity);
    }
  }
}