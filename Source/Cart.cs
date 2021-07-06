using System;

namespace Napilnik.Encapsulation
{
  public class Cart
  {
    private readonly Warehouse _warehouse;
    private readonly ICellStorage _cellStorage = new Warehouse();

    public Cart(Warehouse warehouse)
    {
      if (warehouse == null)
        throw new NullReferenceException(nameof(warehouse));
      
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
        Remove(cell.Good, cell.Count);
    }

    public IOrder GetOrder()
    {
      return new Order(_cellStorage.Cells);
    }
  }
}