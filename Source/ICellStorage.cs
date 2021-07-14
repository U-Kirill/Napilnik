namespace Napilnik.Encapsulation
{
  public interface ICellStorage : IReadOnlyStorage
  {
    void Delive(Good good, int count);

    void Extract(Good good, int count);
  }
}