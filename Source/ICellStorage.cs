namespace Tasks
{
  public interface ICellStorage : ICellStorageView
  {
    void Delive(Good good, int count);

    void Extract(Good good, int count);

  }
}