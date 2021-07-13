namespace Napilnik
{
  public interface IReadOnlyHealth
  {
    int CurrentHealth { get; }
    int MaxHealth { get; }
  }
}