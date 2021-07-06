using System.Collections.Generic;

namespace Napilnik.Encapsulation
{
  public interface IOrder
  {
    IReadOnlyList<Cell> Cells { get; }
    string Paylink { get; }
  }
}