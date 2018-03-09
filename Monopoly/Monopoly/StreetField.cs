using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public class StreetField : Field
  {
    public Groups Group { get; private set; }
    public Costs Cost { get; private set; }
    public int Level { get; private set; }
    private Game _game;
    private Player _owner;
    
    private int RentToPay
    {
      get
      {
        if (_owner != null)
          return Cost.Rent[Level];
        else
          return 0;
      }
    }

    public class Costs
    {
      public int Ground;
      public int House;
      public int[] Rent;
      public int Mortage;
    }

    public StreetField(string name, Groups group, Game game, Costs costs)
    {
      Name = name;
      Group = group;
      Cost = costs;
      _game = game;
    }

    public override void OnEnter()
    {
      PayRent();
    }
    
    private void PayRent()
    {
      if (_owner != null && _owner.Name != _game.CurrentPlayer.Name)
      {
        _game.CurrentPlayer.PayMoney(RentToPay);
        _owner.GetMoney(RentToPay);
      }
    }

    public void Buy()
    {
      if (_owner != null)
        throw new InvalidOperationException("The Street is already owned by Player " + _owner.Name);
      _game.CurrentPlayer.OwnerShip.Add(this);
      _game.CurrentPlayer.PayMoney(Cost.Ground);
      _owner = _game.CurrentPlayer;
    }

    public void LevelUp(Player owner, int levels)
    {
      if (_owner == null)
        throw new InvalidOperationException("Nobody ownes this field");
      if (owner.Name != _owner.Name)
        throw new ArgumentException("This player can not increase the level ");
      if (levels <= 0 || levels + Level > 5)
        throw new ArgumentException("You cant increase the level by this amount");
      owner.PayMoney(Cost.House * levels);
      Level += levels;
    }


  }
}
