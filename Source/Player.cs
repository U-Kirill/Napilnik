using System;

namespace Napilnik
{
  public class Player : IDamageable
  {
    private readonly Health _health;

    public event Action Damaged;

    public Player(int maxHealth)
    {
      _health = new Health(maxHealth);
    }

    public IReadOnlyHealth Health => _health;
    
    public void TakeDamage(int damage)
    {
      _health.Damage(damage);

      Damaged?.Invoke();
    }

    public void Heal(int heal)
    {
      _health.Heal(heal);
    }
  }
}