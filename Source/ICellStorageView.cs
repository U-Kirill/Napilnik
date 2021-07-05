using System.Collections.Generic;

namespace Tasks
{
  public interface ICellStorageView
  {

    IReadOnlyList<Cell> Cells { get; }

    bool CanExtract(Good good, int count);

  }
}