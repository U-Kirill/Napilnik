using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
  public class Task2
  {

    public static void Main(string[] args)
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
warehouse.

      Console.WriteLine(cart.GetOrder().Paylink);
      Console.ReadKey();
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

  public interface ICellStorage
  {
    IReadOnlyList<Cell> Cells { get; }

    void Delive(Good good, int count);

    void Extract(Good good, int count);

  }

  public class Warehouse : ICellStorage
  {
    List<Cell> _cells = new List<Cell>();
    List<Cell> _reserved = new List<Cell>();
    
    
    public IReadOnlyList<Cell> Cells => _cells;

    public void Delive(Good good, int count)
    {
      int index = GetIndex(good);

      if (index != -1)
        _cells[index] = _cells[index].AddItem(count);
      else
       _cells.Add(new Cell(good, count));
    }

    public void Extract(Good good, int count)
    {
      int index = GetIndex(good);
      
      ValidateIndex(index, count, _cells);
      
      _cells[index] = _cells[index].RemoveItem(count);
    }

    public bool CanReserve(Good good, int count)
    {
      int index = GetIndex(good);

      return index != -1 && count <= _cells[index].Count;
    }

    public void AddToReserved(Good good, int count)
    {
      int indexInReserved = GetIndexInReserved(good);

      if (indexInReserved != -1)
        _reserved[indexInReserved] = _cells[indexInReserved].AddItem(count);
      else
        _reserved.Add(new Cell(good, count));
    }

    public void RemoveFromReserved(Good good, int count)
    {
      int indexInReserved = GetIndexInReserved(good);

      ValidateIndex(indexInReserved, count, _reserved);

      _reserved[indexInReserved] = _reserved[indexInReserved].RemoveItem(count);
    }

    private void Reserve(Good good, int count)
    {
      Extract(good, count);

      AddToReserved(good, count);
    }

    private void CancelReserve(Good good, int count)
    {
      RemoveFromReserved(good, count);

      Delive(good, count);
    }

    private void ValidateIndex(int index, int goodCount, IReadOnlyList<Cell> collection)
    {
      if (index == -1)
        throw new InvalidOperationException("Can't remove not reserved item");

      if (collection[index].Count < goodCount)
        throw new InvalidOperationException("Can't remove more when reserved");
    }

    private void ProcessOrder(IOrder order )
    {
      foreach (Cell cell in order.Cells)
      {
        RemoveFromReserved(cell.Good, cell.Count);
        Delive(cell.Good, cell.Count);
      }
    }

    private void CancelOrder(IOrder order)
    {
      foreach (Cell cell in order.Cells)
      {
        int indexInReserved = GetIndexInReserved(cell.Good);

        if (indexInReserved == -1)
          throw new InvalidOperationException("Can't remove not reserved item");
      
        if (_reserved[indexInReserved].Count < cell.Count)
          throw new InvalidOperationException("Can't remove more when reserved");
        
        _reserved[indexInReserved] = _reserved[indexInReserved].RemoveItem(cell.Count);
      }
    }

    private int GetIndex(Good good) => 
      _cells.FindIndex(x => x.Good.Name == good.Name);

    private int GetIndexInReserved(Good good) => 
      _reserved.FindIndex(x => x.Good.Name == good.Name);

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
    
    public Cell RemoveItem(int count)
    {
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof(count));
      
      if (count > Count)
        throw new InvalidOperationException("Can't remove more than contain cell");

      return new Cell(Good, Count - count);
    }
  }

  internal class Shop
  {
    private readonly Warehouse _warehouse;

    public Shop(Warehouse warehouse)
    {
      _warehouse = warehouse;
    }

    public Cart Cart() => 
      new Cart(_warehouse);

  }

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
      if (!_warehouse.CanReserve(good, count))
        throw new InvalidOperationException("Good is not contains in warehouse or not enough amount");

      Reserve(good, count);
    }

    public IOrder GetOrder()
    {
      
      
      return _order;
    }

    public void Cancel()
    {
      foreach (Cell cell in _cellStorage.Cells)
      {
        _warehouse.Re
      }
    }

    private void Reserve(Good good, int count)
    {
      _warehouse.Extract(good, count);
      _warehouse.AddToReserved(good, count);
      _cellStorage.Delive(good, count);
    }
    
    private void CancelReserve(Good good, int count)
    {
      _cellStorage.Extract(good, count);
      _warehouse.RemoveFromReserved(good, count);
      _warehouse.Delive(good, count);
    }

    private class Order : IOrder
    {
      private IPaylinkProvider _paylinkProvider = new RandomPaylinkProvider(10);

      
      public Order()
      {
        Paylink = _paylinkProvider.GetLink();
      }

      public IReadOnlyList<Cell> Cells { get; }

      public object Paylink { get; set; }
    }
  }

  public interface IOrder
  {

    IReadOnlyList<Cell> Cells { get; }
    object Paylink { get; set; }

  }



  public class RandomPaylinkProvider : IPaylinkProvider
  {
    private readonly int _linkLenght;
    
    private Random _random = new Random();
    
    
    public RandomPaylinkProvider(int linkLenght)
    {
      _linkLenght = linkLenght;
    }
    
    public string GetLink() => 
      GetRandomString();

    private string GetRandomString()
    {
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      
      return new string(Enumerable.Repeat(chars, _linkLenght)
        .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
  }

  public interface IPaylinkProvider
  {
    string GetLink();
  }
}