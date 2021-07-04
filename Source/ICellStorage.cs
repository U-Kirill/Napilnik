using System.Collections.Generic;

namespace Tasks
{
  public interface ICellStorage
  {
    IReadOnlyList<Cell> Cells { get; }

    void Delive(Good good, int count);

    void Extract(Good good, int count);

    bool CanExtract(Good good, int count);

  }
}