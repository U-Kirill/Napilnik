using System;

namespace Source
{
  internal class Movement
  {
    public Movement(float directionHorizontal, float directionVertical, float speed)
    {
      DirectionHorizontal = directionHorizontal;
      DirectionVertical = directionVertical;
      Speed = speed;
    }

    public float DirectionHorizontal { get; }

    public float DirectionVertical { get; }

    public float Speed { get; }

    public void Move() => 
      throw new NotImplementedException();

  }
}