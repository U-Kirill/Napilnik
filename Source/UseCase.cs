﻿using System;
using System.Collections;

namespace Napilnik.Encapsulation
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

      DisplayCellStorage(warehouse);

      Cart cart = shop.Cart();
      cart.Add(iPhone12, 4);
      cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе
      
      DisplayCellStorage(cart.Storage);

      Console.WriteLine(cart.GetOrder().Paylink);
      Console.ReadKey();
    }

    private static void DisplayCellStorage(IReadOnlyStorage cellStorage) =>
      Console.WriteLine(cellStorage.ToString());
  }
}