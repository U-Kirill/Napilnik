using System;
using NUnit.Framework;
using Tasks;

namespace NapilnikTests
{
  public class Tests
  {

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
      Player player = new Player(100);
      player.ApplyDamage(10);
      
      Assert.AreEqual(90, player.Health);
    }

    [Test]
    public void Test2()
    {
      Player player = new Player(100);
      player.ApplyDamage(100);
      
      Assert.AreEqual(0, player.Health);
    }
    
    [Test]
    public void Test3()
    {
      Player player = new Player(100);
      player.ApplyDamage(200);
      
      Assert.AreEqual(0, player.Health);
    }
    
    [Test]
    public void Test4()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new Player(0));
    }
    
    [Test]
    public void Test5()
    {
      Player player = new Player(100);
      Bot bot= new Bot(new Stats
      {
        Bullets = 10,
        Damage = 50
      });
      
      bot.OnSeePlayer(player);
      
      Assert.AreEqual(50, player.Health);
    }
    
    [Test]
    public void Test6()
    {
      Player player = new Player(100);
      
      Stats stats = new Stats
      {
        Bullets = 10,
        Damage = 50
      };
      
      Bot bot= new Bot(stats);
      stats.Damage = 100;
      bot.OnSeePlayer(player);
      
      Assert.AreEqual(50, player.Health);
    }
  }
  internal class Stats : IWeaponStats
  {

    public int Damage { get; set; }
    public int Bullets { get; set; }

  }
}

