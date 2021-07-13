using System;

namespace Napilnik
{
  public class Health : IReadOnlyHealth
  {
    public Health(int maxHealth)
    {
      if (maxHealth <= 0)
        throw new ArgumentOutOfRangeException(nameof(maxHealth));

      MaxHealth = maxHealth;
      CurrentHealth = MaxHealth;
    }

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; }

    public void Damage(int damage)
    {
      if (damage < 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      int overDamage = Math.Max(damage - CurrentHealth, 0);
      CurrentHealth -= damage - overDamage;
    }
  }
}