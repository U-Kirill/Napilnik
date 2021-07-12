namespace Source
{
  public interface ILoggerCondition
  {
    bool CanWrite(string message);
  }
}