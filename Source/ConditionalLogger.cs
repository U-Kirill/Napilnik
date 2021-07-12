using System;

namespace Source
{
  public class ConditionalLogger : ILogger
  {
    private readonly ILogger _logger;
    private readonly ILoggerCondition _condition;

    public ConditionalLogger(ILogger logger, ILoggerCondition condition)
    {
      _logger = logger;
      _condition = condition;
    }

    public void Write(string message)
    {
      if(_condition.CanWrite(message))
        _logger.Write(message);
    }
  }
}