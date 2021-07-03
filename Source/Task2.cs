using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
  public class Task2
  {

    public static void Maint(string[] args)
    {
      Good iPhone12 = new Good("IPhone 12");
      Good iPhone11 = new Good("IPhone 11");

      Warehouse warehouse = new Warehouse();

      Shop shop = new Shop(warehouse);

      warehouse.Delive(iPhone12, 10);
      warehouse.Delive(iPhone11, 1);

//Вывод всех товаров на складе с их остатком

      Cart cart = shop.Cart();
      cart.Add(iPhone12, 4);
      cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

//Вывод всех товаров в корзине

      Console.WriteLine(cart.Order().Paylink);
    }

  }

  public class Good
  {
    public string Name { get; }

    public Good(string name)
    {
      Name = name;
    }
  }

  public class Warehouse
  {
    List<Cell> _cells = new List<Cell>();
    
    
    public IReadOnlyList<Cell> Cells => _cells;

    public void Delive(Good good, int count)
    {
      int index = _cells.FindIndex(x => x.Good.Name == good.Name);

      if (index != -1)
        _cells[index] = _cells[index].AddItem(count);
      else
       _cells.Add(new Cell(good, count));
    }

    public bool CanExtract(Good good, int count)
    {
      throw new NotImplementedException();
    }

    public void Extract(Good good, int count)
    {
      throw new NotImplementedException();
    }

  }
  public struct Cell
  {
    public Cell(Good good, int count = 0)
    {
      if(count < 0)
        throw new ArgumentOutOfRangeException(nameof(count));
        
      Good = good;
      Count = count;
    }

    public Good Good { get; }
    public int Count { get; }

    public Cell AddItem(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));

      return new Cell(Good, Count + count);
    }
  }

  internal class Shop
  {
    private readonly Warehouse _warehouse;

    public Shop(Warehouse warehouse)
    {
      _warehouse = warehouse;
      throw new System.NotImplementedException();
    }

    public Cart Cart() => 
      new Cart(_warehouse);

  }

  public class Cart
  {
    private readonly Warehouse _warehouse;
    private readonly Warehouse _cartWarehouse;

    public Cart(Warehouse warehouse)
    {
      _warehouse = warehouse;
      _cartWarehouse = new Warehouse();
    }

    public void Add(Good good, int count)
    {
      if (!_warehouse.CanExtract(good, count))
        throw new InvalidOperationException();
      
      _cartWarehouse.Delive(good, count);
    }

    public Order Order()
    {
      foreach (Cell cell in _cartWarehouse.Cells) 
        _warehouse.Extract(cell.Good, cell.Count);

      return new Order(_cartWarehouse.Cells);
    }

  }

  public class Order
  {
    public IReadOnlyList<Cell> CartWarehouseCells { get; }

    public Order(IReadOnlyList<Cell> cartWarehouseCells)
    {
      CartWarehouseCells = cartWarehouseCells;
    }

    public object Paylink { get; set; }

  }
}