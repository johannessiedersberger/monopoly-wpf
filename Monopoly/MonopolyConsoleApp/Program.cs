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

      bool isGameOver = false;
      while (!isGameOver)
      {
        
        Console.Write("Throw Dice "+ game.CurrentPlayer.Name  + " (Enter)");
        Console.ReadLine();
        game.NextTurn();
        DisplayGame(game);

        bool nextPlayer = false;
        while (!nextPlayer)
        {
          DisplayGame(game);
          Console.Write("Action: Buy(b), LevelUp(l), Mortage(m), SellHosue(s), continue(c):");
          string input = Console.ReadLine();
          if (input == "b")
            Buy(game);
          if (input == "l")
            LevelUp(game);
          if (input == "m")
            Mortage(game);
          if (input == "s")
            SellHouse(game);
          if (input == "c")
            nextPlayer = true;
        }
      }
    }

    private static void Buy(Game game)
    {
      ((StreetField)game.Fields[game.PlayerPos[game.CurrentPlayer]]).Buy(game.CurrentPlayer);
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
