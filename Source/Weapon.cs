using System;

namespace Source
{
  internal class Weapon
  {
    public Weapon(float cooldown, int damage)
    {
      Cooldown = cooldown;
      Damage = damage;
    }

    public float Cooldown { get; }

    public int Damage { get; }

    public void Shoot() =>
      throw new NotImplementedException();

    public bool IsReloading() =>
      throw new NotImplementedException();

  }
}