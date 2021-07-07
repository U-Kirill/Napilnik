namespace Napilnik
{
  public interface IWeapon
  {
    int CurrentBullets { get; }
    int Damage { get; }

    void Fire(IDamageable player);

    bool CanFire();
  }
}