using System.Collections.Generic;

namespace Napilnik.Encapsulation
{
  public interface ICellStorageView
  {
    IReadOnlyList<Cell> Cells { get; }
    
    bool CanExtract(Good good, int count);
  }
}