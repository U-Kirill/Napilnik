namespace Tasks
{
  using System;

  public interface IReadOnlyWeaponStats
  {
    int Damage { get; }
    int Bullets { get; }
  }
  
  public class Weapon
  {
    private Stats _stats;

    
    public Weapon(IReadOnlyWeaponStats stats)
    {
      if (stats.Bullets < 0)
        throw new ArgumentOutOfRangeException(nameof(stats.Bullets));

      if (stats.Damage < 0)
        throw new ArgumentOutOfRangeException(nameof(stats.Damage));

      _stats = new Stats(stats);
    }

    public IReadOnlyWeaponStats CurrentStats => _stats;

    public void Fire(IDamageable player)
    {
      if (!CanFire())
        throw new InvalidOperationException("Can't fire without bullets");

      _stats.Bullets -= 1;
      player.ApplyDamage(_stats.Damage);
    }

    public bool CanFire() =>
      _stats.Bullets > 0;
    
    private struct Stats : IReadOnlyWeaponStats
    {
      public Stats(IReadOnlyWeaponStats weaponStats)
      {
        Damage = weaponStats.Damage;
        Bullets = weaponStats.Bullets;
      }

      public int Damage { get; }
      public int Bullets { get; set; }
    }
  }

  public class Player : IDamageable
  {
    public Player(int health)
    {
      if (health <= 0)
        throw new ArgumentOutOfRangeException(nameof(health));
      
      Health = health;
    }

    public int Health { get; private set; }

    public void ApplyDamage(int damage)
    {
      if (damage < 0)
        throw new ArgumentOutOfRangeException(nameof(damage));

      int overdamage = Math.Max(damage - Health, 0);
      Health -= damage + overdamage;
    }
  }

  public interface IDamageable
  {
    void ApplyDamage(int damage);
  }

  public class Bot
  {
    private Weapon _weapon;

    public Bot(IReadOnlyWeaponStats stats)
    {
      _weapon = new Weapon(stats);
    }

    public void OnSeePlayer(Player player)
    {
      _weapon.Fire(player);
    }
  }
}