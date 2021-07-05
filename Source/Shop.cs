namespace Tasks
{
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
}