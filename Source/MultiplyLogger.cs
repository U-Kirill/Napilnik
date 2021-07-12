namespace Source
{
  public class MultiplyLogger : ILogger
  {
    private readonly ILogger[] _loggers;

    public MultiplyLogger(params ILogger[] loggers)
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