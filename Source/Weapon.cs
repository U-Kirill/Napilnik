namespace Source
{
  internal class Weapon
  {
    private readonly int _bulletsPerShoot;
    private int _bullets;

    public Weapon(int bullets, int bulletsPerShoot)
    {
      _bulletsPerShoot = bulletsPerShoot;
      _bullets = bullets;
    }

    public bool CanShoot() => GetBulletsAfterShoot() > 0;

    public void Shoot() => _bullets = GetBulletsAfterShoot();

    private int GetBulletsAfterShoot() => _bullets - _bulletsPerShoot;
  }
}