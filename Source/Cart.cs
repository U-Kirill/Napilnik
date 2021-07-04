using System;
using System.Collections.Generic;

namespace Tasks
{
  public class Cart
  {
    private readonly Warehouse _warehouse;
    private readonly ICellStorage _cellStorage = new Warehouse();


    public Cart(Warehouse warehouse)
    {
      _warehouse = warehouse;
    }

    public IReadOnlyList<Cell> Cells => _cellStorage.Cells;
    
    public void Add(Good good, int count)
    {
      if (!_warehouse.CanExtract(good, count))
        throw new InvalidOperationException("Good is not contains in warehouse or not enough amount");

      Reserve(good, count);
    }

    public void Remove(Good good, int count)
    {
      if (!_cellStorage.CanExtract(good, count))
        throw new InvalidOperationException("Good is not contains in cart or not enough amount");

      CancelReserve(good, count);
    }

    public void Cancel()
    {
      foreach (Cell cell in _cellStorage.Cells) 
        CancelReserve(cell.Good, cell.Count);
    }

    public IOrder GetOrder()
    {
      return new Order(_cellStorage.Cells);
    }

    private void Reserve(Good good, int count)
    {
      _warehouse.Extract(good, count);
      _cellStorage.Delive(good, count);
    }
    
    private void CancelReserve(Good good, int count)
    {
      _cellStorage.Extract(good, count);
      _warehouse.Delive(good, count);
    }

    private class Order : IOrder
    {
      private readonly IPaylinkProvider _paylinkProvider = new RandomPaylinkProvider(10);
      private readonly IReadOnlyList<Cell> _cellStorageCells;


      public Order(IReadOnlyList<Cell> cellStorageCells)
      {
        _cellStorageCells = cellStorageCells;
        Paylink = _paylinkProvider.GetLink();
      }

      public IReadOnlyList<Cell> Cells { get; }

      public object Paylink { get; set; }
    }
  }
}