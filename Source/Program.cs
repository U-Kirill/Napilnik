using System;

namespace Source
{
  public class Program
  {
    public static void Main()
    {
      Pathfinder consoleLogPathfinder = new Pathfinder(new ConsoleLogger());
      Pathfinder fileLogPathfinder = new Pathfinder(new FileLogger());
      Pathfinder conditionalFileLogPathfinder = new Pathfinder(new ConditionalLogger(new FileLogger(), new DayOfWeekCondition(DayOfWeek.Friday)));
      Pathfinder conditionalConsoleLogPathfinder = new Pathfinder(new ConditionalLogger(new ConsoleLogger(), new DayOfWeekCondition(DayOfWeek.Friday)));
      Pathfinder multiplyConsoleLogPathfinder = new Pathfinder(new MultiplyLogger(new ConsoleLogger(), new ConditionalLogger(new FileLogger(), new DayOfWeekCondition(DayOfWeek.Friday))));
    }
  }
}