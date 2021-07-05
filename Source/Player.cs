using System;

namespace Napilnik
{
  public class Player : IDamageable
  {
    public event Action Damaged;

    public Player(int health)
    {
      if (health <= 0)
        throw new ArgumentOutOfRangeException(nameof(health));

      Health = health;
    }

    public int Health { get; private set; }

    public void TakeDamage(int damage)
    {
      if (damage < 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      int overdamage = Math.Max(damage - Health, 0);
      Health -= damage - overdamage;

      Damaged?.Invoke();
    }
  }
}