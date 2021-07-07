using System.Collections.Generic;

namespace Napilnik.Encapsulation
{
  public interface IReadOnlyStorage
  {
    IReadOnlyList<Cell> Cells { get; }
    
    bool CanExtract(Good good, int count);
  }
}