namespace Napilnik
{
  public interface IWeapon
  {
    int Bullets { get; }
    int Damage { get; }

    void Fire(IDamageable player);

    bool CanFire();
  }
}