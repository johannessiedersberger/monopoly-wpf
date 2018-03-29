using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  class TrainstationField : IRentableField
  {
    public int RentToPay { get; private set; }
    public bool IsMortage { get; private set; }
    public Player Owner { get; private set; }
    public Groups Group { get; private set; }
    public string Name { get; private set; }

    public class Costs
    {
      
    }

    public void Buy(Player player)
    {
      //if (Owner != null)
      //  throw new InvalidOperationException("The Street is already owned by Player " + Owner.Name);

      //player.AddToOwnerShip(this);
      //player.PayMoney(Cost.Ground);
      //Owner = _game.CurrentPlayer;
    }

    public void OnEnter(Player player)
    {
      
    }

    public void PayOffMortage(Player player)
    {
      
    }

    public void TakeMortage(Player player)
    {
      
    }
  }
}
