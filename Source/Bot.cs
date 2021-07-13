using System;

namespace Napilnik
{
  public class Bot
  {
    private readonly IWeapon _weapon;

    public Bot(IWeapon weapon)
    {
      if (weapon == null)
        throw new NullReferenceException(nameof(weapon));

      _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
      if (_weapon.CanFire())
        _weapon.Fire(player);
    }
  }
}