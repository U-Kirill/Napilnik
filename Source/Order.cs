using System;
using System.Collections.Generic;

namespace Napilnik.Encapsulation
{
  public class Order : IOrder
  {
    private readonly IPaylinkProvider _paylinkProvider = new RandomPaylinkProvider(10);

    public Order(IReadOnlyList<Cell> cells)
    {
      if (cells == null)
        throw new NullReferenceException(nameof(cells));

      Cells = cells;
      Paylink = _paylinkProvider.GetLink();
    }

    public IReadOnlyList<Cell> Cells { get; }
    public string Paylink { get; }
  }
}