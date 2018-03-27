using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;

namespace Test
{
  class Test
  {
    [Test]
    public void TestNextTurn()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY")});
      game.NextTurn();

      Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[0]));
      game.NextTurn();
      Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[1]));
      game.NextTurn();
      Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[0]));
    }

    [Test]
    public void TestBuyCurrentField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0],1);
      game.BuyCurrentStreet(game.Players[0]);
      Assert.That(game.Players[0].OwnerShip[0].Name, Is.EqualTo(FieldNames.OldKentRoad));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 60));
    }

    [Test]
    public void TestUpdateLevel()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0], 1);
      game.BuyCurrentStreet(game.Players[0]);
      game.Players[0].OwnerShip[0].LevelUp(game.Players[0],5);
      Assert.That(game.Players[0].OwnerShip[0].Level, Is.EqualTo(5));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 60 - 5 * 50));
    }

    [Test]
    public void TestPayRent()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0], 1);
      game.BuyCurrentStreet(game.Players[0]);
      game.Players[0].OwnerShip[0].LevelUp(game.Players[0], 5);
      
      game.SetPlayerPos(game.Players[1], pos: 1);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 - 250));
    }

    [Test]
    public void TestSetPlayerPosition()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0], 1);
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(1));
    }

    [Test]
    public void TestCrossedStartField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0],4);
      game.NextTurn();
      Assert.That(game.Players[0].Money >= 1700, Is.EqualTo(true));
    }
  }
}
