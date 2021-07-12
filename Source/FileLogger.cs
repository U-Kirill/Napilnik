using System.IO;

namespace Source
{
  public class FileLogger : ILogger
  {
    public void Write(string message) => 
      File.WriteAllText("log.txt", message);
  }
}