namespace Source
{
  public class Player
  {
    public Player(string name, int coins, int army)
    {
      Name = name;
      Coins = coins;
      Army = army;
    }

    public string Name { get; }

    public int Coins { get; }

    public int Army { get; }
  }
}