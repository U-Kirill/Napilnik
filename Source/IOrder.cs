using System.Collections.Generic;

namespace Tasks
{
  public interface IOrder
  {
    IReadOnlyList<Cell> Cells { get; }
    string Paylink { get; }
  }
}