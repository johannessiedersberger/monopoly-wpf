using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{

  #region fieldNames

  public static class FieldNames
  {
    public static string OldKentRoad = "Old Kent Road";
    public static string WhiteChapelRoad = "Whitechapel Road";
    public static string TheAngelIslington = "The Angel, Isligton";
    public static string EustonRoad = "Euston Road";
    public static string PentonvilleRoad = "Pentonville Road";
    public static string PallMall = "Pall Mall";
    public static string WhiteHall = "White Hall";
    public static string NorthumrldAvenue = "Northumrl'd Avenue";
    public static string BowStreet = "Bow Street";
    public static string MarlBoroughStreet = "MarlBorough Street";
    public static string WineStreet = "Vine Street";
    public static string Strand = "Strand";
    public static string FleetStreet = "Fleet Street";
    public static string TrafalgarSquare = "Trafalgar Square";
    public static string LeicesterSquare = "Leicester Square";
    public static string CoventryStreet = "Conventry Street";
    public static string Piccadilly = "Piccadilly";
    public static string RegentStreet = "Regent Street";
    public static string OxfordStreet = "Oxford Street";
    public static string BondStreet = "Bond Street";
    public static string ParkLane = "Park Lane";
    public static string Mayfair = "Mayfair";

    public static string KingsCrossStation = "Kings Cross";
    public static string MaryLeboneStation = "Marylebone Station";
    public static string FencurchStation = "Fenchruchv Station";
    public static string LiverpoolStation = "Liverpool Station";

    public static string WaterWorks = "Water Works";
    public static string EnergyCompany = "Energy Company";

    public static string CommunityChest = "Community Chest";
    public static string Chance = "Chance";
    public static string GoToJail = "Go To Jail";
    public static string FreeParking = "Free Parking";
    public static string Jail = "Jail";

    public static string SuperTax = "Super Tax";
    public static string IncomeTax = "Income Tax";

    public static string Start = "Start";
  }

  #endregion

  #region Group 
  public enum Groups
  {
    Brown,
    LightBlue,
    Pink,
    Orange,
    Red,
    Yellow,
    Green,
    DarkBlue,
    TrainStation,
    Supplier
  };
  #endregion

  class FieldCreator
  {
    public static IField[] Create(Game game)
    {
      return new IField[] {
        CreateStartField(game),
        CreateOldKentRoad(game),
        CreateWhiteChapelRoad(game),
        CreateParkLane(game),
        CreateMayfair(game),
      };
    }

    private static StartField CreateStartField(Game game)
    {
      return new StartField(FieldNames.Start,game);
    }

    private static IRentableField CreateOldKentRoad(Game game)
    {
      return new StreetField(FieldNames.OldKentRoad, Groups.Brown, game, new StreetField.Costs()
      {
        Ground = 60,
        House = 50,
        Rent = new[] { 2, 10, 30, 90, 160, 250 },
        Mortage = 30
      });
    }

    private static StreetField CreateWhiteChapelRoad(Game game)
    {
      return new StreetField(FieldNames.WhiteChapelRoad, Groups.Brown, game, new StreetField.Costs()
      {
        Ground = 60,
        House = 50,
        Rent = new[] { 4, 20, 60, 180, 320, 450 },
        Mortage = 30
      });
    }

    private static StreetField CreateParkLane(Game game)
    {
      return new StreetField(FieldNames.ParkLane, Groups.DarkBlue, game, new StreetField.Costs()
      {
        Ground = 350,
        House = 200,
        Rent = new[] { 35, 175, 500, 1100, 1300, 1500 },
        Mortage = 175
      });
    }

    private static StreetField CreateMayfair(Game game)
    {
      return new StreetField(FieldNames.Mayfair, Groups.DarkBlue, game, new StreetField.Costs()
      {
        Ground = 400,
        House = 200,
        Rent = new[] { 50, 200, 600, 1400, 1700, 2000 },
        Mortage = 200
      });
    }
   
  }
}
