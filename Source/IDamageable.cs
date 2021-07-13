using System;

namespace Napilnik
{
  public interface IDamageable
  {
    event Action Damaged;

    void TakeDamage(int damage);
  }
}