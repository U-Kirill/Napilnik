namespace Source
{
  public class MultyplyLogger : ILogger
  {
    private readonly ILogger[] _loggers;

    public MultyplyLogger(params ILogger[] loggers)
    {
      _loggers = loggers;
    }

    public void Write(string message)
    {
      foreach (ILogger logger in _loggers)
        logger.Write(message);
    }
  }
}