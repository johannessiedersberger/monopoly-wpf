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
      StreetField field1 = (StreetField)game.Fields[1];
      game.SetPlayerPos(game.Players[0],1);
      game.BuyCurrentStreet(game.Players[0]);

      Assert.That(game.Players[0].OwnerShip[0].Name, Is.EqualTo(FieldNames.OldKentRoad));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 60));

      game.SetPlayerPos(game.Players[1], 1);
      Assert.That(() => game.BuyCurrentStreet(game.Players[1]), Throws.InvalidOperationException);
    }

    [Test]
    public void TestUpdateLevel()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = (StreetField)game.Fields[1];
      StreetField field2 = (StreetField)game.Fields[2];
      StreetField field3 = (StreetField)game.Fields[3];
      field1.Buy(game.Players[0]);
      Assert.That(() => field1.LevelUp(game.Players[0], 5), Throws.InvalidOperationException);
      field2.Buy(game.Players[0]);
      field1.LevelUp(game.Players[0],5);
      Assert.That(field1.Level, Is.EqualTo(5));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 2*60 - 5 * 50));
      Assert.That(() => field3.LevelUp(game.Players[0], 5), Throws.InvalidOperationException);
      Assert.That(() => field1.LevelUp(game.Players[1], 5), Throws.ArgumentException);
      Assert.That(() => field1.LevelUp(game.Players[0], 7), Throws.ArgumentException);
    }

    [Test]
    public void TestPayRent()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = (StreetField)game.Fields[1];
      StreetField field2 = (StreetField)game.Fields[2];
      StreetField field3 = (StreetField)game.Fields[3];
      field1.Buy(game.Players[0]);
      field2.Buy(game.Players[0]);
      field1.LevelUp(game.Players[0], 5);
      
      game.SetPlayerPos(game.Players[1], pos: 1);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 - 250));
      Assert.That(field3.RentToPay, Is.EqualTo(0));
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

    [Test]
    public void TestTakeMortage()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = (StreetField)game.Fields[1];
      StreetField field2 = (StreetField)game.Fields[2];
      StreetField field3 = (StreetField)game.Fields[3];
      field1.Buy(game.Players[0]);
      
      field1.TakeMortage(game.Players[0]);
      Assert.That(field1.IsMortage, Is.EqualTo(true));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 60 + 30));

      game.SetPlayerPos(game.Players[1], 1);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500));

      Assert.That(() => field3.TakeMortage(game.Players[1]), Throws.InvalidOperationException);
      Assert.That(() => field1.TakeMortage(game.Players[0]), Throws.InvalidOperationException);
      Assert.That(() => field1.TakeMortage(game.Players[1]), Throws.InvalidOperationException);
      field2.Buy(game.Players[0]);
      field1.LevelUp(game.Players[0], 5);
      Assert.That(() => field1.TakeMortage(game.Players[0]), Throws.InvalidOperationException);
    }

    [Test]
    public void TestPayOffMortage()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      StreetField field2 = ((StreetField)game.Fields[2]);

      field1.Buy(game.Players[0]);
      field1.TakeMortage(game.Players[0]);
      Assert.That(field1.IsMortage, Is.EqualTo(true));
      field1.PayOffMortage(game.Players[0]);
      Assert.That(field1.IsMortage, Is.EqualTo(false));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 60 + 30 - 33));
      Assert.That(() => field1.PayOffMortage(game.Players[1]), Throws.InvalidOperationException);
      field2.Buy(game.Players[0]);
      Assert.That(() => field2.PayOffMortage(game.Players[0]), Throws.InvalidOperationException);
    }

    [Test]
    public void TestSellHouse()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      StreetField field2 = ((StreetField)game.Fields[2]);

      field1.Buy(game.Players[0]);
      field2.Buy(game.Players[0]);
      field1.LevelUp(game.Players[0], 5);
      Assert.That(((StreetField)game.Fields[1]).Level, Is.EqualTo(5));
      field1.SellHouse(game.Players[0], 5);
      Assert.That(field1.Level, Is.EqualTo(0));
      Assert.That(() => field1.SellHouse(game.Players[1],1), Throws.InvalidOperationException);
      Assert.That(() => field1.SellHouse(game.Players[0], 1), Throws.InvalidOperationException);
    }

    [Test]
    public void TestTrainStationField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      TrainstationField field5 = ((TrainstationField)game.Fields[5]);
      TrainstationField field6 = ((TrainstationField)game.Fields[6]);
      
      field5.Buy(game.Players[0]);
      field6.Buy(game.Players[0]);
      Assert.That(() => field5.Buy(game.Players[0]), Throws.InvalidOperationException);
      game.SetPlayerPos(game.Players[1], 5);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 - 50));
      Assert.That(() => field5.SetRentToPay(100), Throws.ArgumentException);

    }


    [Test]
    public void TestTakeMortageTrainStation()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      TrainstationField field5 = ((TrainstationField)game.Fields[5]);
      TrainstationField field6 = ((TrainstationField)game.Fields[6]);
      field5.Buy(game.Players[0]);

      field5.TakeMortage(game.Players[0]);
      Assert.That(field5.IsMortage, Is.EqualTo(true));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 200 + 100));

      game.SetPlayerPos(game.Players[1], 1);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500));

      Assert.That(() => field6.TakeMortage(game.Players[1]), Throws.InvalidOperationException);
      Assert.That(() => field5.TakeMortage(game.Players[0]), Throws.InvalidOperationException);
      Assert.That(() => field5.TakeMortage(game.Players[1]), Throws.InvalidOperationException);
      field6.Buy(game.Players[0]);
    }

    [Test]
    public void TestPayOffMortageTrainStation()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      TrainstationField field5 = ((TrainstationField)game.Fields[5]);
      TrainstationField field6 = ((TrainstationField)game.Fields[6]);

      field5.Buy(game.Players[0]);
      field5.TakeMortage(game.Players[0]);
      Assert.That(field5.IsMortage, Is.EqualTo(true));
      field5.PayOffMortage(game.Players[0]);
      Assert.That(field5.IsMortage, Is.EqualTo(false));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 200 + 100 - 110));
      Assert.That(() => field5.PayOffMortage(game.Players[1]), Throws.InvalidOperationException);
      field6.Buy(game.Players[0]);
      Assert.That(() => field5.PayOffMortage(game.Players[0]), Throws.InvalidOperationException);
    }
  }
}
