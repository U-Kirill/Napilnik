using System;

namespace Source
{
    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount)
        {
            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Id = id;
            Amount = amount;
        }
    }
}