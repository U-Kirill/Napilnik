using System;
using System.Collections.Generic;
using System.Text;

namespace Napilnik.Encapsulation
{
  public class Warehouse : ICellStorage
  {
    private readonly List<Cell> _cells = new List<Cell>();
    
    public IReadOnlyList<Cell> Cells => _cells;

    public void Delive(Good good, int count)
    {
      int index = GetIndex(good);

      if (index != -1)
        _cells[index] = _cells[index].Add(count);
      else
        _cells.Add(new Cell(good, count));
    }

    public bool CanExtract(Good good, int count)
    {
      int index = GetIndex(good);

      return index != -1 && _cells[index].CanRemove(count);
    }

    public void Extract(Good good, int count)
    {
      int index = GetIndex(good);

      if (index == -1)
        throw new InvalidOperationException($"Can't remove not existed item {good?.Name}");

      _cells[index] = _cells[index].Remove(count);
    }

    private int GetIndex(Good good) => 
      _cells.FindIndex(x => x.Good.Name == good?.Name);

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();

      foreach (Cell cell in _cells) 
        builder.AppendLine(cell.ToString());

      return builder.ToString();
    }
  }
}