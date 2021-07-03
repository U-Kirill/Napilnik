using System;
using Napilnik;
using NUnit.Framework;

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
  }
}