using System;

namespace Napilnik
{
  public class Weapon : IWeapon
  {
    public Weapon(int damage, int bullets)
    {
      if (bullets < 0)
        throw new ArgumentOutOfRangeException(nameof(bullets));

      if (damage < 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      Bullets = bullets;
      Damage = damage;
    }

    public int Bullets { get; private set; }
    public int Damage { get; }

    public void Fire(IDamageable player)
    {
      if (player == null)
        throw new NullReferenceException(nameof(player));

      if (!CanFire())
        throw new InvalidOperationException("Can't fire without bullets");

      Bullets -= 1;
      player.TakeDamage(Damage);
    }

    public bool CanFire() =>
      Bullets > 0;
  }
}