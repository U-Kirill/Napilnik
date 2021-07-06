namespace Napilnik.Encapsulation
{
  public interface ICellStorage : ICellStorageView
  {
    void Delive(Good good, int count);

    void Extract(Good good, int count);
  }
}