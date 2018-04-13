using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;
using Monopoly.Cards;
namespace Test
{
  class Test
  {
    #region GamePlay
    [Test]
    public void TestNextTurn()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.NextPlayer();

      Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[0]));
      game.NextPlayer();
      Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[1]));
      game.NextPlayer();
      Assert.That(game.CurrentPlayer, Is.EqualTo(game.Players[0]));
    }

    [Test]
    public void TestCrossedStartField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0], game.Fields.Count()-1);
      game.NextPlayer();
      game.GoForward(game.CurrentPlayer);
      Assert.That(game.Players[0].Money >= 1700, Is.EqualTo(true));
    }

    [Test]
    public void PlayerIsBankrupt()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      StreetField field2 = ((StreetField)game.Fields[2]);
      StreetField field3 = ((StreetField)game.Fields[3]);
      StreetField field4 = ((StreetField)game.Fields[4]);

      game.Players[0].GetMoney(20000);
      game.Players[1].PayMoney(1500);

      field1.Buy(game.Players[0]);
      field2.Buy(game.Players[0]);
      field3.Buy(game.Players[0]);
      field4.Buy(game.Players[0]);

      field1.LevelUp(game.Players[0], 5);
      field2.LevelUp(game.Players[0], 5);
      field3.LevelUp(game.Players[0], 5);
      field4.LevelUp(game.Players[0], 5);

      Assert.That(() => game.Fields[4].OnEnter(game.Players[1]), Throws.TypeOf(typeof(BankruptException)));
    }

    [Test]
    public void TestGameOver()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      field1.Buy(game.Players[0]);
      game.Players[1].PayMoney(game.Players[1].Money);
      try
      {
        field1.OnEnter(game.Players[1]);
      }
      catch (BankruptException)
      {
        game.RemovePlayer(game.Players[1]);
        Assert.That(game.CurrentPlayer.Removed, Is.EqualTo(true));
      }
      Assert.That(game.IsGameOver, Is.EqualTo(true));
    }

    [Test]
    public void PlayerDoesNotHaveEnoughMoney()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      StreetField field2 = ((StreetField)game.Fields[2]);
      StreetField field3 = ((StreetField)game.Fields[3]);
      StreetField field4 = ((StreetField)game.Fields[4]);

      game.Players[0].GetMoney(20000);
      game.Players[1].GetMoney(20000);

      field1.Buy(game.Players[0]);
      field2.Buy(game.Players[1]);

      game.Players[1].PayMoney(game.Players[1].Money);
      Assert.That(() => game.Fields[1].OnEnter(game.Players[1]), Throws.TypeOf(typeof(NotEnoughMoneyException)));
    }



    #endregion

    #region Player
    [Test]
    public void TestSetPlayerPosition()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      game.SetPlayerPos(game.Players[0], 1);
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(1));
    }

    [Test]
    public void TestRemovePlayer()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      field1.Buy(game.Players[0]);
      game.RemovePlayer(game.Players[0]);
      Assert.That(game.Players.Count(), Is.EqualTo(1));
      Assert.That(game.PlayerPos.Count(), Is.EqualTo(1));
      Assert.That(game.CurrentPlayer.Removed, Is.EqualTo(true));
      Assert.That(game.AuctionFields[0].Name, Is.EqualTo(FieldNames.OldKentRoad));
    }
    #endregion

    #region StreetField
    [Test]
    public void TestBuyCurrentField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = (StreetField)game.Fields[1];
      game.SetPlayerPos(game.Players[0], 1);
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
      field1.LevelUp(game.Players[0], 5);
      Assert.That(field1.Level, Is.EqualTo(5));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 2 * 60 - 5 * 50));
      Assert.That(() => field3.LevelUp(game.Players[0], 5), Throws.InvalidOperationException);
      Assert.That(() => field1.LevelUp(game.Players[1], 5), Throws.ArgumentException);
      Assert.That(() => field1.LevelUp(game.Players[0], 7), Throws.ArgumentException);
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
      Assert.That(() => field1.SellHouse(game.Players[1], 1), Throws.InvalidOperationException);
      Assert.That(() => field1.SellHouse(game.Players[0], 1), Throws.InvalidOperationException);
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
    public void TestExChangeFieldMoney()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      field1.Buy(game.Players[0]);
      field1.ExchangeField(game.Players[0], game.Players[1], 60);
      Assert.That(game.Players[0].OwnerShip.Count(), Is.EqualTo(0));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500));

      Assert.That(game.Players[1].OwnerShip[0].Name, Is.EqualTo(FieldNames.OldKentRoad));
      Assert.That(game.Players[1].Money, Is.EqualTo(1440));
      Assert.That(field1.Owner.Name, Is.EqualTo(game.Players[1].Name));
    }

    [Test]
    public void TestExChangeFieldwithField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      StreetField field2 = ((StreetField)game.Fields[2]);
      field1.Buy(game.Players[0]);
      field2.Buy(game.Players[1]);
      field1.ExchangeField(field1.Owner, game.Players[1], field2);
      Assert.That(field1.Owner.Name, Is.EqualTo(game.Players[1].Name));
      Assert.That(field2.Owner.Name, Is.EqualTo(game.Players[0].Name));
    }

    [Test]
    public void TestBuyInAuction()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      field1.BuyInAuction(game.Players[0], 20);
      Assert.That(game.Players[0].OwnerShip[0].Name, Is.EqualTo(FieldNames.OldKentRoad));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 20));
    }

    [Test]
    public void TestAuctionField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      game.StartAuction(new List<IRentableField> { field1 });
      game.AuctionField(field1, new Dictionary<Player, int>
      {
        {game.Players[0],100 },
        {game.Players[1],200 }
      });
      game.FinishAuction();
      Assert.That(game.Players[1].OwnerShip[0].Name, Is.EqualTo(FieldNames.OldKentRoad));
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 - 200));

    }

    #endregion

    #region TrainStationFieldTest
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

    [Test]
    public void TestExChangeTrainStationFieldMoney()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      TrainstationField field5 = ((TrainstationField)game.Fields[5]);
      field5.Buy(game.Players[0]);
      field5.ExchangeField(game.Players[0], game.Players[1], 60);
      Assert.That(game.Players[0].OwnerShip.Count(), Is.EqualTo(0));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 200 + 60));

      Assert.That(game.Players[1].OwnerShip[0].Name, Is.EqualTo(FieldNames.KingsCrossStation));
      Assert.That(game.Players[1].Money, Is.EqualTo(1440));
      Assert.That(field5.Owner.Name, Is.EqualTo(game.Players[1].Name));
    }

    [Test]
    public void TestExChangeTrainStationFieldwithField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      TrainstationField field5 = ((TrainstationField)game.Fields[5]);
      field1.Buy(game.Players[0]);
      field5.Buy(game.Players[1]);
      field5.ExchangeField(field5.Owner, game.Players[0], field1);
      Assert.That(field1.Owner.Name, Is.EqualTo(game.Players[1].Name));
      Assert.That(field5.Owner.Name, Is.EqualTo(game.Players[0].Name));
    }

    [Test]
    public void TestTrainStationFieldBuyInAuction()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      TrainstationField field5 = ((TrainstationField)game.Fields[5]);
      field5.BuyInAuction(game.Players[0], 20);
      Assert.That(game.Players[0].OwnerShip[0].Name, Is.EqualTo(FieldNames.KingsCrossStation));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 20));
    }
    #endregion

    #region SupplierField

    [Test]
    public void TestBuyAndRentSupplierField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      SupplierField field7 = (SupplierField)game.Fields[7];
      SupplierField field8 = (SupplierField)game.Fields[8];

      field7.Buy(game.Players[0]);
      field8.Buy(game.Players[0]);

      game.SetLastThrow(game.Players[1], new List<int[]>
      {
        new int[]{6,6 },
      });
      field7.OnEnter(game.Players[1]);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 - (120)));
    }

    [Test]
    public void TestTakeMortageSupplierField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      SupplierField field7 = (SupplierField)game.Fields[7];
      SupplierField field8 = (SupplierField)game.Fields[8];
     
      field7.Buy(game.Players[0]);

      field7.TakeMortage(game.Players[0]);
      Assert.That(field7.IsMortage, Is.EqualTo(true));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 150 + 75));

      game.SetPlayerPos(game.Players[1], 1);
      Assert.That(game.Players[1].Money, Is.EqualTo(1500));

      Assert.That(() => field8.TakeMortage(game.Players[1]), Throws.InvalidOperationException);
      Assert.That(() => field7.TakeMortage(game.Players[0]), Throws.InvalidOperationException);
      Assert.That(() => field7.TakeMortage(game.Players[1]), Throws.InvalidOperationException);
    }

    [Test]
    public void TestPayOffMortageSupplierField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      SupplierField field7 = (SupplierField)game.Fields[7];
      SupplierField field8 = (SupplierField)game.Fields[8];

      field7.Buy(game.Players[0]);
      field7.TakeMortage(game.Players[0]);
      Assert.That(field7.IsMortage, Is.EqualTo(true));
      field7.PayOffMortage(game.Players[0]);
      Assert.That(field7.IsMortage, Is.EqualTo(false));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 150 + 75- 82));
      Assert.That(() => field7.PayOffMortage(game.Players[1]), Throws.InvalidOperationException);
      field8.Buy(game.Players[0]);
      Assert.That(() => field8.PayOffMortage(game.Players[0]), Throws.InvalidOperationException);
    }

    [Test]
    public void TestBuySupplierFieldInAuction()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      SupplierField field7 = ((SupplierField)game.Fields[7]);
      field7.BuyInAuction(game.Players[0], 20);
      Assert.That(game.Players[0].OwnerShip[0].Name, Is.EqualTo(FieldNames.WaterWorks));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 20));
    }

    [Test]
    public void TestExChangeSupplierFieldwithField()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      StreetField field1 = ((StreetField)game.Fields[1]);
      SupplierField field7 = ((SupplierField)game.Fields[7]);
      field1.Buy(game.Players[0]);
      field7.Buy(game.Players[1]);
      field7.ExchangeField(field7.Owner, game.Players[0], field1);
      Assert.That(field1.Owner.Name, Is.EqualTo(game.Players[1].Name));
      Assert.That(field7.Owner.Name, Is.EqualTo(game.Players[0].Name));
    }

    [Test]
    public void TestexChangeSupplierFieldMoney()
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      SupplierField field7 = ((SupplierField)game.Fields[7]);
      field7.Buy(game.Players[0]);
      field7.ExchangeField(game.Players[0], game.Players[1], 60);
      Assert.That(game.Players[0].OwnerShip.Count(), Is.EqualTo(0));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 150 + 60));

      Assert.That(game.Players[1].OwnerShip[0].Name, Is.EqualTo(FieldNames.WaterWorks));
      Assert.That(game.Players[1].Money, Is.EqualTo(1440));
      Assert.That(field7.Owner.Name, Is.EqualTo(game.Players[1].Name));
    }
    #endregion

    #region Prison
    [Test]
    public void TestGoToJailField()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      game.Fields[9].OnEnter(game.Players[0]);
      Assert.That(game.Players[0].InPrison, Is.EqualTo(true));
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(10));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500));
    }

    [Test]
    public void TestPlayerThrowsDouble()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
 
      game.Fields[9].OnEnter(game.Players[0]);
      Assert.That(game.Players[0].InPrison, Is.EqualTo(true));

      game.GoForward(game.CurrentPlayer, new int[] { 1, 1 });
      Assert.That(game.Players[0].InPrison, Is.EqualTo(false));
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(1));
    }

    [Test]
    public void TestPlayerTried3Times()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
   
      game.Fields[9].OnEnter(game.Players[0]);
      Assert.That(game.Players[0].InPrison, Is.EqualTo(true));

      game.GoForward(game.CurrentPlayer, new int[] { 1, 2 });
      game.GoForward(game.CurrentPlayer, new int[] { 1, 2 });
      game.GoForward(game.CurrentPlayer, new int[] { 1, 2 });
      Assert.That(game.Players[0].InPrison, Is.EqualTo(false));
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(2));
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 +200- 50));
    }

    [Test]
    public void PlayerPayFineImmediately()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      game.Fields[9].OnEnter(game.Players[0]);
      game.PayFineImmediately(game.Players[0]);
      game.GoForward(game.Players[0], new int[] { 1, 2 });
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 +200- 50));
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(2));
    }
    #endregion

    #region Cards
    [Test]
    public void TestMoneyCard()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y") });
      MoneyCard card = new MoneyCard("Pay 50", -50, game);
      card.UseCard(game.Players[0]);
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 50));
    }

    [Test]
    public void TestGetMoneyFromPlayersCard()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      GetMoneyFromPlayersCard card = new GetMoneyFromPlayersCard("", 50, game);

      card.UseCard(game.Players[0]);
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 + 2 * 50));
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 - 50));
      Assert.That(game.Players[2].Money, Is.EqualTo(1500 - 50));
    }

    [Test]
    public void TestStreetRepairCard()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      StreetField field1 = (StreetField)game.Fields[1];
      StreetField field2 = (StreetField)game.Fields[2];

      field1.Buy(game.Players[0]);
      field2.Buy(game.Players[0]);

      field1.LevelUp(game.Players[0], 5);
      field2.LevelUp(game.Players[0], 4);

      StreetRepairCard card = new StreetRepairCard("", 50, 100, game);
      card.UseCard(game.Players[0]);
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 2*60 - 9*50 - 4 * 50 - 1 * 100));
    }

    [Test]
    public void TestPayPlayersCard()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      PayPlayersCard card = new PayPlayersCard("", 50, game);
      card.UseCard(game.Players[0]);
      Assert.That(game.Players[0].Money, Is.EqualTo(1500 - 2 * 50));
      Assert.That(game.Players[1].Money, Is.EqualTo(1500 + 50));
      Assert.That(game.Players[2].Money, Is.EqualTo(1500 + 50));
    }

    [Test]
    public void TestGoToJailCard()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      GoToJailCard card = new GoToJailCard("", game);
      card.UseCard(game.Players[0]);
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(10));
      Assert.That(game.Players[0].InPrison, Is.EqualTo(true));
    }
    [Test]
    public void TestGoToCard()
    {
      Game game = new Game(new Player[] { new Player("X"), new Player("Y"), new Player("Z") });
      int position = 3;
      game.SetPlayerPos(game.Players[0], game.Fields.Count() - 1);
      MoveToCard card = new MoveToCard("", position, game);
      card.UseCard(game.Players[0]);
      Assert.That(game.PlayerPos[game.Players[0]], Is.EqualTo(position));
      Assert.That(game.Players[0].Money, Is.EqualTo(1700));
    }
    
   
    #endregion
  }
}
