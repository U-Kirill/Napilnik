namespace Source
{
  internal class Player
  {
    private Weapon _weapon;
    private Movement _movement;

    public Player(Weapon weapon, Movement movement)
    {
      _weapon = weapon;
      _movement = movement;
    }

    public string Name { get; }

    public int Age { get; }

    public void Move() =>
      _movement.Move();

    public void Attack()
    {
      if (!_weapon.IsReloading())
        _weapon.Shoot();
    }
  }
}