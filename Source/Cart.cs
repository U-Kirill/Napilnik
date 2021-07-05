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

    public ICellStorageView Storage => _cellStorage;
    
    public void Add(Good good, int count)
    {
      _warehouse.Extract(good, count);
      _cellStorage.Delive(good, count);
    }

    public void Remove(Good good, int count)
    {
      _cellStorage.Extract(good, count);
      _warehouse.Delive(good, count);
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
      
    }
    
    private void CancelReserve(Good good, int count)
    {
      
    }

    private class Order : IOrder
    {
      private readonly IPaylinkProvider _paylinkProvider = new RandomPaylinkProvider(10);
      

      public Order(IReadOnlyList<Cell> cellStorageCells)
      {
        Cells = cellStorageCells;
        Paylink = _paylinkProvider.GetLink();
      }

      public IReadOnlyList<Cell> Cells { get; }

      public string Paylink { get; }
    }
  }
}