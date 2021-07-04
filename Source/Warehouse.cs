using System;
using System.Collections.Generic;

namespace Tasks
{
  public class Warehouse : ICellStorage
  {
    List<Cell> _cells = new List<Cell>();

    
    public IReadOnlyList<Cell> Cells => _cells;

    public void Delive(Good good, int count)
    {
      int index = GetIndex(good);

      if (index != -1)
        _cells[index] = _cells[index].IncreaseQuantity(count);
      else
        _cells.Add(new Cell(good, count));
    }

    public bool CanExtract(Good good, int count)
    {
      int index = GetIndex(good);

      return index != -1 && count <= _cells[index].Count;
    }

    public void Extract(Good good, int count)
    {
      int index = GetIndex(good);
      
      if (index == -1)
        throw new InvalidOperationException("Can't remove not existed item");

      _cells[index] = _cells[index].ReduceQuantity(count);
    }

    private int GetIndex(Good good) => 
      _cells.FindIndex(x => x.Good.Name == good.Name);
  }
}