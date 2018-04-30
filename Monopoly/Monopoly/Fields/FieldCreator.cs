using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Fields;

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
    Supplier,
    change,
    communityChest,
    tax,

  };
  #endregion

  class FieldCreator
  {
    public static IField[] Create(Game game)
    {
      return new IField[] {
        CreateStartField(game),
        CreateOldKentRoad(game),
        CreateCommunityChestField(game),
        CreateWhiteChapelRoad(game),
        CreateIncomeTaxField(game),
        CreateKingsCrossStation(game),
        CreateTheAngelIsligton(game),
        CreateChangeField(game),
        CreateEustonRoad(game),
        CreatePentonvilleRoad(game),
        CreateJailField(game),
        CreatePallMall(game),
        CreateEnergyCompany(game),
        CreateWhitehall(game),
        CreateNorthumrldAvenue(game),
        CreateMaryLeboneStation(game),
        CreateBowStreet(game),
        CreateCommunityChestField(game),
        CreateMarlboroughStreet(game),
        CreateWineStreet(game),
        CreateFreeParking(game),
        CreateStrand(game),
        CreateChangeField(game),
        CreateFleetStreet(game),
        CreateTrafalgarSquare(game),
        CreateFenchurchStation(game),
        CreateLeicestserSquare(game),
        CreateConventryStreet(game),
        CreateWaterWorks(game),
        CreatePicadilly(game),
        CreateGoToJailField(game),
        CreateRegentStreet(game),
        CreateOxFordStreat(game),
        CreateCommunityChestField(game),
        CreateBondStreet(game),
        CreateLiverPoolStation(game),
        CreateChangeField(game),
        CreateParkLane(game),
        CreateSuperTaxField(game),
        CreateMayfair(game),
      };
    }

    #region start
    private static StartField CreateStartField(Game game)
    {
      return new StartField(FieldNames.Start, game);
    }
    #endregion
    #region brown
    private static StreetField CreateOldKentRoad(Game game)
    {
      return new StreetField(FieldNames.OldKentRoad, Groups.Brown,game, new StreetField.Costs()
      {
        Ground = 60,
        House = 50,
        Rent = new[] { 2, 10, 30, 90, 160, 250 },
        Mortage = 30
      });
    }

    private static StreetField CreateWhiteChapelRoad(Game game)
    {
      return new StreetField(FieldNames.WhiteChapelRoad, Groups.Brown,game, new StreetField.Costs()
      {
        Ground = 60,
        House = 50,
        Rent = new[] { 4, 20, 60, 180, 320, 450 },
        Mortage = 30
      });
    }
    #endregion
    #region lightBlue
    private static StreetField CreateTheAngelIsligton(Game game)
    {
      return new StreetField(FieldNames.TheAngelIslington, Groups.LightBlue,game, new StreetField.Costs()
      {
        Ground = 100,
        House = 50,
        Rent = new[] { 6, 30, 90, 270, 400, 550 },
        Mortage = 50
      });
    }

    private static StreetField CreateEustonRoad(Game game)
    {

      return new StreetField(FieldNames.EustonRoad, Groups.LightBlue,game, new StreetField.Costs()
      {
        Ground = 100,
        House = 50,
        Rent = new[] { 6, 30, 90, 270, 400, 550 },
        Mortage = 50
      });
    }

    private static StreetField CreatePentonvilleRoad(Game game)
    {
      return new StreetField(FieldNames.PentonvilleRoad, Groups.LightBlue, game,new StreetField.Costs()
      {
        Ground = 120,
        House = 50,
        Rent = new[] { 8, 40, 100, 300, 450, 600 },
        Mortage = 60
      });
    }
    #endregion
    #region pink
    private static StreetField CreatePallMall(Game game)
    {
      return new StreetField(FieldNames.PallMall, Groups.Pink,game, new StreetField.Costs()
      {
        Ground = 140,
        House = 100,
        Rent = new[] { 10, 50, 150, 450, 625, 750 },
        Mortage = 70
      });
    }

    private static StreetField CreateWhitehall(Game game)
    {
      return new StreetField(FieldNames.WhiteHall, Groups.Pink,game, new StreetField.Costs()
      {
        Ground = 140,
        House = 100,
        Rent = new[] { 10, 50, 150, 450, 625, 750 },
        Mortage = 70
      });
    }

    private static StreetField CreateNorthumrldAvenue(Game game)
    {
      return new StreetField(FieldNames.NorthumrldAvenue, Groups.Pink,game, new StreetField.Costs()
      {
        Ground = 160,
        House = 100,
        Rent = new[] { 12, 60, 180, 500, 700, 900 },
        Mortage = 80
      });
    }
    #endregion
    #region orange
    private static StreetField CreateBowStreet(Game game)
    {
      return new StreetField(FieldNames.BowStreet, Groups.Orange,game, new StreetField.Costs()
      {
        Ground = 180,
        House = 100,
        Rent = new[] { 14, 70, 200, 550, 700, 900 },
        Mortage = 90
      });
    }

    private static StreetField CreateMarlboroughStreet(Game game)
    {
      return new StreetField(FieldNames.MarlBoroughStreet, Groups.Orange,game, new StreetField.Costs()
      {
        Ground = 180,
        House = 100,
        Rent = new[] { 14, 70, 200, 550, 700, 900 },
        Mortage = 90
      });
    }

    private static StreetField CreateWineStreet(Game game)
    {
      return new StreetField(FieldNames.WineStreet, Groups.Orange,game, new StreetField.Costs()
      {
        Ground = 200,
        House = 100,
        Rent = new[] { 16, 80, 220, 600, 800, 1000 },
        Mortage = 100
      });
    }
    #endregion
    #region red
    private static StreetField CreateStrand(Game game)
    {
      return new StreetField(FieldNames.Strand, Groups.Red,game, new StreetField.Costs()
      {
        Ground = 220,
        House = 150,
        Rent = new[] { 18, 90, 250, 700, 875, 1050 },
        Mortage = 110
      });
    }

    private static StreetField CreateFleetStreet(Game game)
    {
      return new StreetField(FieldNames.FleetStreet, Groups.Red,game, new StreetField.Costs()
      {
        Ground = 220,
        House = 150,
        Rent = new[] { 18, 90, 250, 700, 875, 1050 },
        Mortage = 110
      });
    }

    private static StreetField CreateTrafalgarSquare(Game game)
    {
      return new StreetField(FieldNames.TrafalgarSquare, Groups.Red,game, new StreetField.Costs()
      {
        Ground = 240,
        House = 150,
        Rent = new[] { 20, 100, 300, 750, 925, 1100 },
        Mortage = 120
      });
    }
    #endregion
    #region yellow
    private static StreetField CreateLeicestserSquare(Game game)
    {
      return new StreetField(FieldNames.LeicesterSquare, Groups.Yellow,game, new StreetField.Costs()
      {
        Ground = 260,
        House = 150,
        Rent = new[] { 22, 110, 330, 800, 975, 1150 },
        Mortage = 130
      });
    }

    private static StreetField CreateConventryStreet(Game game)
    {
      return new StreetField(FieldNames.CoventryStreet, Groups.Yellow,game, new StreetField.Costs()
      {
        Ground = 260,
        House = 150,
        Rent = new[] { 22, 110, 330, 800, 975, 1150 },
        Mortage = 130
      });
    }

    private static StreetField CreatePicadilly(Game game)
    {
      return new StreetField(FieldNames.Piccadilly, Groups.Yellow,game, new StreetField.Costs()
      {
        Ground = 280,
        House = 150,
        Rent = new[] { 24, 120, 360, 850, 1025, 1200 },
        Mortage = 140
      });
    }
    #endregion
    #region green
    private static StreetField CreateRegentStreet(Game game)
    {
      return new StreetField(FieldNames.RegentStreet, Groups.Green,game, new StreetField.Costs()
      {
        Ground = 300,
        House = 200,
        Rent = new[] { 26, 130, 390, 900, 1100, 1275 },
        Mortage = 150
      });
    }

    private static StreetField CreateOxFordStreat(Game game)
    {
      return new StreetField(FieldNames.OxfordStreet, Groups.Green,game, new StreetField.Costs()
      {
        Ground = 300,
        House = 200,
        Rent = new[] { 26, 130, 390, 900, 1100, 1275 },
        Mortage = 150
      });
    }

    private static StreetField CreateBondStreet(Game game)
    {
      return new StreetField(FieldNames.BondStreet, Groups.Green,game, new StreetField.Costs()
      {
        Ground = 320,
        House = 200,
        Rent = new[] { 28, 150, 450, 1000, 1200, 1400 },
        Mortage = 160
      }
      );
    }
    #endregion
    #region darkblue
    private static StreetField CreateParkLane(Game game)
    {
      return new StreetField(FieldNames.ParkLane, Groups.DarkBlue,game, new StreetField.Costs()
      {
        Ground = 350,
        House = 200,
        Rent = new[] { 35, 175, 500, 1100, 1300, 1500 },
        Mortage = 175
      });
    }

    private static StreetField CreateMayfair(Game game)
    {
      return new StreetField(FieldNames.Mayfair, Groups.DarkBlue,game, new StreetField.Costs()
      {
        Ground = 400,
        House = 200,
        Rent = new[] { 50, 200, 600, 1400, 1700, 2000 },
        Mortage = 200
      });
    }
    #endregion
    #region trainstations
    private static TrainstationField CreateKingsCrossStation(Game game)
    {
      return new TrainstationField(FieldNames.KingsCrossStation, Groups.TrainStation, game, new TrainstationField.Costs()
      {
        Ground = 200,
        Rent = new int[] { 25, 50, 100, 200 },
        Mortage = 100
      });
    }

    private static TrainstationField CreateMaryLeboneStation(Game game)
    {
      return new TrainstationField(FieldNames.MaryLeboneStation, Groups.TrainStation, game, new TrainstationField.Costs()
      {
        Ground = 200,
        Rent = new int[] { 25, 50, 100, 200 },
        Mortage = 100
      });
    }
    private static TrainstationField CreateFenchurchStation(Game game)
    {
      return new TrainstationField(FieldNames.FencurchStation, Groups.TrainStation, game, new TrainstationField.Costs()
      {
        Ground = 200,
        Rent = new int[] { 25, 50, 100, 200 },
        Mortage = 100
      });
    }
    private static TrainstationField CreateLiverPoolStation(Game game)
    {
      return new TrainstationField(FieldNames.LiverpoolStation, Groups.TrainStation, game, new TrainstationField.Costs()
      {
        Ground = 200,
        Rent = new int[] { 25, 50, 100, 200 },
        Mortage = 100
      });
    }

    #endregion
    #region suppliers
    private static SupplierField CreateWaterWorks(Game game)
    {
      return new SupplierField(FieldNames.WaterWorks, Groups.Supplier, game, new SupplierField.Costs()
      {
        Mortage = 75,
        Ground = 150,
      });
    }

    private static SupplierField CreateEnergyCompany(Game game)
    {
      return new SupplierField(FieldNames.EnergyCompany, Groups.Supplier, game, new SupplierField.Costs()
      {
        Mortage = 75,
        Ground = 150
      });
    }
    #endregion
    #region cardFields
    private static CommunityChestField CreateCommunityChestField(Game game)
    {
      return new CommunityChestField(FieldNames.CommunityChest, Groups.communityChest, game);
    }

    private static ChangeField CreateChangeField(Game game)
    {
      return new ChangeField(FieldNames.Chance, Groups.communityChest, game);
    }
    #endregion
    #region JailFields
    private static GoToJailField CreateGoToJailField(Game game)
    {
      return new GoToJailField(FieldNames.GoToJail, game);
    }

    private static JailField CreateJailField(Game game)
    {
      return new JailField(FieldNames.Jail, game);
    }
    #endregion
    #region tax
    private static TaxField CreateIncomeTaxField(Game game)
    {
      return new TaxField(FieldNames.IncomeTax, Groups.tax, game, 200);
    }
    private static TaxField CreateSuperTaxField(Game game)
    {
      return new TaxField(FieldNames.SuperTax, Groups.tax, game, 100);
    }
    #endregion
    #region freeparking
    private  static FreeParkingField CreateFreeParking(Game game)
    {
      return new FreeParkingField();
    }
    #endregion

  }
}
