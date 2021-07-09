namespace Source
{
  public interface IRoom
  {
    void MakeReady(Player player);
    void SendMessage(Player sender, string message);
    string ReadAllMessages(Player reader);
  }
}