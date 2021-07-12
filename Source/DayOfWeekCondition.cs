using System;

namespace Source
{
  public class DayOfWeekCondition : ILoggerCondition
  {
    private readonly DayOfWeek _targetDay;

    public DayOfWeekCondition(DayOfWeek targetDay)
    {
      _targetDay = targetDay;
    }

    public bool CanWrite(string message) => 
      DateTime.Now.DayOfWeek == _targetDay;
  }
}