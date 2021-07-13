using System;

namespace Source
{
  internal class Movement
  {
    private readonly Direction _direction;

    public Movement(Direction direction, float speed)
    {
      _direction = direction;
      Speed = speed;
    }
    
    public float Speed { get; }

    public void Move() =>
      throw new NotImplementedException();

  }
}