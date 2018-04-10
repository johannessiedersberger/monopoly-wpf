using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly;

namespace MonopolyConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });
      DisplayGame(game);

      #region test
      StreetField field1 = ((StreetField)game.Fields[1]);
      StreetField field2 = ((StreetField)game.Fields[2]);
      StreetField field3 = ((StreetField)game.Fields[3]);
      StreetField field4 = ((StreetField)game.Fields[4]);

      field1.Buy(game.Players[1]);
      field2.Buy(game.Players[1]);

      game.Players[0].GetMoney(20000);

      field3.Buy(game.Players[0]);
      field4.Buy(game.Players[0]);

      field3.LevelUp(game.Players[0], 5);
      field4.LevelUp(game.Players[0], 5);


      #endregion // TEST!

      bool isGameOver = false;
      while (!isGameOver)
      {
        if(game.Players.Count() == 2) // Test
          game.Players[1].PayMoney(game.Players[1].Money);

        try // Get NextPlayer, Throw Dice, Go Forward
        {
          game.NextPlayer();
          DisplayGame(game);
          Console.Write("Throw Dice " + game.CurrentPlayer.Name + " (Enter)");
          Console.ReadLine();
          game.GoForward(game.CurrentPlayer);
        }
        catch (Exception ex) // Player has not enough money
        {
          if (ex.GetType() == typeof(NotEnoughMoneyException)) // Player has to sell something
          {
            int neededAmount = int.Parse(ex.Message);
            while (game.CurrentPlayer.Money - neededAmount < 0)
            {
              DisplayGame(game);
              Console.WriteLine("You have to sell something, because you need " + (neededAmount - game.CurrentPlayer.Money) + "$" + ": house(h), groundMortage(g)");
              string input = Console.ReadLine();
              if (input == "h")
                SellHouse(game);
              if (input == "g")
                Mortage(game);
            }
            game.CallOnEnter(game.CurrentPlayer);

          }
          else if (ex.GetType() == typeof(BankruptException)) // Player is Bankrupt
          {
            DisplayGame(game);
            Console.WriteLine("YOU ARE BANKRUPT PLAYER " + game.CurrentPlayer.Name);
            Console.ReadLine();
            game.RemovePlayer(game.CurrentPlayer);
            Auction(game);
          }
        }

        DisplayGame(game);

        //Actions
        bool nextPlayer = false;
        while (!nextPlayer && game.CurrentPlayer.Removed == false) // Dont ask for Action if CurrentPlayer Has been Removed
        {
          DisplayGame(game);

          Console.WriteLine("You have thrown " + game.GetLastThrow(game.CurrentPlayer)[0] + " " + game.GetLastThrow(game.CurrentPlayer)[1]);
          Console.Write("Action: Buy(b), LevelUp(l), Mortage(m), SellHosue(s), exchangeField(e) continue(Enter):");
          string input = Console.ReadLine();
          try
          {
            if (input == "b")
              Buy(game);
            if (input == "l")
              LevelUp(game);
            if (input == "m")
              Mortage(game);
            if (input == "s")
              SellHouse(game);
            if (input == "e")
              ExchangeField(game);
            if (input == "")
              nextPlayer = true;
          }
          catch (Exception ex)
          {
            Console.WriteLine("Oops Something went wrong: " + ex.Message + " Press (Enter) to Continue");
            string s = Console.ReadLine();
          }
        }
        if(game.CurrentPlayer.Removed == false)
        {
          //Auction
          IField currentField = game.Fields[game.PlayerPos[game.CurrentPlayer]];
          if (currentField.GetType() == typeof(StreetField) && ((IRentableField)currentField).Owner == null)
          {
            game.StartAuction(new List<IRentableField> { (IRentableField)currentField });
            Auction(game);
          }
        }
      }
    }

    private static void Auction(Game game)
    {
      Dictionary<Player, int> bids = new Dictionary<Player, int>();
      foreach(IRentableField field in game.AuctionFields)
      {
        Console.Write("Is somebody interested in the " + field.Name + " j/n");
        string interst = Console.ReadLine();
        if(interst == "j")
        {
          foreach (var player in game.Players)
          {
            Console.Write("is " + player.Name + " interested in the " + field.Name + " ja(j) / Nein (n)");
            string decision = Console.ReadLine();
            if (decision == "j")
            {
              Console.Write("What do you want to pay for the " + field.Name + " Proeperty: ");
              int bid = int.Parse(Console.ReadLine());
              bids.Add(player, bid);
            }
          }
          game.AuctionField(field, bids);
          bids.Clear();
        }
      }
    }

    private static void Buy(Game game)
    {
      ((IRentableField)game.Fields[game.PlayerPos[game.CurrentPlayer]]).Buy(game.CurrentPlayer);
    }

    private static void LevelUp(Game game)
    {
      for (int s = 0; s < game.CurrentPlayer.OwnerShip.Count(); s++)
      {
        StreetField street = (StreetField)game.CurrentPlayer.OwnerShip[s];
        Console.WriteLine(s + ". " + street.Name + "Level " + street.Level + ", Costs: " + street.Cost.House);
      }
      Console.Write("StreetNum: ");
      int streetNum = int.Parse(Console.ReadLine());
      StreetField streetToUpdate = (StreetField)game.CurrentPlayer.OwnerShip[streetNum];
      Console.Write("Level: ");
      int level = int.Parse(Console.ReadLine());
      streetToUpdate.LevelUp(game.CurrentPlayer, level);
    }


    private static void Mortage(Game game)
    {
      for (int s = 0; s < game.CurrentPlayer.OwnerShip.Count(); s++)
      {
        StreetField street = (StreetField)game.CurrentPlayer.OwnerShip[s];
        Console.WriteLine(s + ". " + street.Name + "Level " + street.Level + ", Costs: " + street.Cost.House);
      }
      Console.Write("StreetNum: ");
      int streetNum = int.Parse(Console.ReadLine());
      StreetField streetMortage = (StreetField)game.CurrentPlayer.OwnerShip[streetNum];
      Console.Write("TakeMortage(t) PayOffMortage(p): ");
      string input = Console.ReadLine();
      if (input == "t")
        streetMortage.TakeMortage(game.CurrentPlayer);
      else if (input == "p")
        streetMortage.PayOffMortage(game.CurrentPlayer);
    }

    private static void SellHouse(Game game)
    {
      for (int s = 0; s < game.CurrentPlayer.OwnerShip.Count(); s++)
      {
        StreetField street = (StreetField)game.CurrentPlayer.OwnerShip[s];
        Console.WriteLine(s + ". " + street.Name + "Level " + street.Level + ", Costs: " + street.Cost.House);
      }
      Console.Write("StreetNum: ");
      int streetNum = int.Parse(Console.ReadLine());
      StreetField streetHouse = (StreetField)game.CurrentPlayer.OwnerShip[streetNum];
      Console.Write("Amount of Houses to Sell (1-5):");
      int amount = int.Parse(Console.ReadLine());
      streetHouse.SellHouse(game.CurrentPlayer, amount);
    }

    private static void ExchangeField(Game game)
    {
      //Property
      Console.WriteLine("Choose the Property you want to Exchange");
      for (int s = 0; s < game.CurrentPlayer.OwnerShip.Count(); s++)
      {
        IRentableField field = (StreetField)game.CurrentPlayer.OwnerShip[s];
        Console.WriteLine(s + ". " + field.Name);
      }
      Console.Write("PropertyNum: ");
      int propetyIndex = int.Parse(Console.ReadLine());

      //Player
      Console.WriteLine("Choose the Player you want to Exchange With");
      for (int i = 0; i < game.Players.Count(); i++)
      {
        Console.WriteLine(i + ". " + game.Players[i].Name);
      }
      int playerNum = int.Parse(Console.ReadLine());

      //Money Or Field
      Console.WriteLine("Do you want to exchange with Money(m) or another Property(p)");
      string decision = Console.ReadLine();


      if(decision == "m")
      {
        //Amont of Money
        Console.Write("How Much Money: ");
        int amount = int.Parse(Console.ReadLine());
        //Exchange
        game.CurrentPlayer.OwnerShip[propetyIndex].ExchangeField(game.CurrentPlayer, game.Players[playerNum], amount);
      }
      else if(decision == "p")
      {
        //Property
        Console.WriteLine("Choose the Property the other player wants to Pay with");
        for (int s = 0; s < game.Players[playerNum].OwnerShip.Count(); s++)
        {
          Console.WriteLine(s + ". " + game.Players[playerNum].OwnerShip[s]);
        }
        Console.WriteLine("propNum: ");
        int propNum = int.Parse(Console.ReadLine());
        game.CurrentPlayer.OwnerShip[propetyIndex].ExchangeField(game.CurrentPlayer, game.Players[playerNum], game.Players[playerNum].OwnerShip[propNum]);
      }
    }

    private static void DisplayGame(Game game)
    {
      Console.Clear();
      DisplayFields(game);
      Console.WriteLine();
      DisplayPlayers(game);
    }

    private static void DisplayFields(Game game)
    {
      for (int f = 0; f < game.Fields.Count(); f++)
      {
        if (game.Fields[f].GetType() == typeof(StreetField))
          DisplayStreetFieldData(game, f);
        //else if (game.Fields[f].GetType() == typeof(TrainstationField))
        //  DisplayTrainStationFieldData(game, f);
        else
          DisplayNormalFieldData(game, f);
        DisplayPlayersOnField(game, f);
      }
    }

    private static void DisplayStreetFieldData(Game game, int pos)
    {
      StreetField streetField = (StreetField)game.Fields[pos];
      Console.Write(streetField.Name + ", Mortage: " + streetField.IsMortage + ", Level: " + streetField.Level + ", RentToPay: " + streetField.RentToPay);
      if (streetField.Owner != null)
        Console.Write(", Owner: " + streetField.Owner.Name);
    }

    //private static void DisplayTrainStationFieldData(Game game, int pos)
    //{
    //  TrainstationField streetField = (TrainstationField)game.Fields[pos];
    //  Console.Write(streetField.Name + ", Mortage: " + streetField.IsMortage  + ", RentToPay: " + streetField.RentToPay);
    //  if (streetField.Owner != null)
    //    Console.Write(", Owner: " + streetField.Owner.Name);
    //}

    private static void DisplayNormalFieldData(Game game, int pos)
    {
      Console.Write(game.Fields[pos].Name);
    }

    private static void DisplayPlayersOnField(Game game, int pos)
    {
      List<Player> playersOnField = GetPlayerOnField(game, pos);
      if (playersOnField.Count != 0)
      {
        Console.Write(", OnField: ");
        foreach (Player p in playersOnField)
          Console.Write(p.Name + " ");
      }
      Console.WriteLine();
    }

    private static List<Player> GetPlayerOnField(Game game, int pos)
    {
      List<Player> playersOnField = new List<Player>();
      foreach (Player player in game.Players)
      {
        if (game.PlayerPos[player] == pos)
          playersOnField.Add(player);
      }
      return playersOnField;
    }

    private static void DisplayPlayers(Game game)
    {
      for (int f = 0; f < game.Players.Count(); f++)
      {
        Console.Write(game.Players[f].Name + ", Money: " + game.Players[f].Money);
        DisPlayOwnerShip(game, f);
        Console.WriteLine();
      }
      if (game.CurrentPlayer.Removed == false) // If Player has been removed dont display him
        Console.WriteLine("CurrentPlayer: " + game.CurrentPlayer.Name);
    }

    private static void DisPlayOwnerShip(Game game, int pos)
    {
      if (game.Players[pos].OwnerShip.Count() != 0)
      {
        Console.Write(", Ownership: ");
        foreach (IRentableField field in game.Players[pos].OwnerShip)
          Console.Write(field.Name + ", ");
      }
    }
  }
}
