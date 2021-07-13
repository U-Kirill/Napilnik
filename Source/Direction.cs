namespace Source
{
  internal class Direction
  {
    public Direction(float directionHorizontal, float directionVertical)
    {
      DirectionHorizontal = directionHorizontal;
      DirectionVertical = directionVertical;
    }

    public float DirectionHorizontal { get; }

    public float DirectionVertical { get; }
  }
}