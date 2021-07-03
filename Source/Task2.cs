using System;
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

  internal class Warehouse
  {
    List<Cell> _cells = new List<Cell>();
    
    public void Delive(Good good, int count)
    {
      
    }
  }

  internal class Cell
  {

  }

  internal class Shop
  {

    public Shop(Warehouse warehouse)
    {
      throw new System.NotImplementedException();
    }

    public Cart Cart()
    {
      throw new System.NotImplementedException();
    }

  }

  public class Cart
  {

    public void Add(Good iPhone12, int p1)
    {
      throw new System.NotImplementedException();
    }

    public Order Order()
    {
      throw new System.NotImplementedException();
    }

  }

  public class Order
  {

    public object Paylink { get; set; }

  }
}