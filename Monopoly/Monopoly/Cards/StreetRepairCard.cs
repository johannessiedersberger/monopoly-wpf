using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class StreetRepairCard : ICard
  {
    public string Description { get; private set; }
    private Game _game;
    private int _costsPerHouse;
    private int _costsperHotel;

    public StreetRepairCard(string description, int costsPerHouse, int costsPerHotel, Game game)
    {
      Description = description;
      _costsPerHouse = costsPerHouse;
      _costsperHotel = costsPerHotel;
      _game = game;
    }

    public void UseCard(Player player)
    {
      int houses = 0;
      int hotels = 0;
      foreach(IRentableField field in player.OwnerShip)
      {
        if(field.GetType() == typeof(StreetField))
        {
          int level = ((StreetField)field).Level;
          if (level < 5)
            houses += level;
          else // level 5
            hotels++;
        }
      }
      player.PayMoney(houses * _costsPerHouse + hotels * _costsperHotel);
    }
  }
}
