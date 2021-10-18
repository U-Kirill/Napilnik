namespace Source
{
  public class Program
  {
    public static void Main()
    {
      Game game = new Game();
    }
  }

  public interface ILobby
  {
    int MaxPlayers { get; }
    int ReadyPlayersCount { get; }
    bool CanConnect();
  }

  
  public interface IPlayer
  {
    bool IsReady { get; }
  }
}