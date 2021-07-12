namespace Source
{
  public class Pathfinder
  {
    private ILogger _logger;

    public Pathfinder(ILogger logger)
    {
      _logger = logger;
    }

    public void Find() => 
      _logger.Write("I Find!");

  }
}