using System;

namespace Napilnik
{
  public class Weapon : IWeapon
  {
    private readonly int _bulletsPerShoot = 1;

    public Weapon(int damage, int bullets)
    {
      if (damage < 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      CurrentBullets = bullets;
      Damage = damage;
    }

    public int CurrentBullets { get; private set; }
    public int Damage { get; }

    private int BulletsAfterShoot => CurrentBullets - _bulletsPerShoot;

    public void Fire(IDamageable player)
    {
      if (player == null)
        throw new NullReferenceException(nameof(player));

      if (!CanFire())
        throw new InvalidOperationException("Can't fire without bullets");

      CurrentBullets = BulletsAfterShoot;
      player.TakeDamage(Damage);
    }

    public bool CanFire() =>
      BulletsAfterShoot >= 0;
  }
}