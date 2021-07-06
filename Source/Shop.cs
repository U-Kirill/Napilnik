using System;

namespace Napilnik.Encapsulation
{
  internal class Shop
  {
    private readonly Warehouse _warehouse;

    public Shop(Warehouse warehouse)
    {
      if (warehouse == null)
        throw new NullReferenceException(nameof(warehouse));
      
      _warehouse = warehouse;
    }

    public Cart Cart() => 
      new Cart(_warehouse);
  }
}