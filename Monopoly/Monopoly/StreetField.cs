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
    public bool IsMortage { get; private set; }
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

    
    public override void OnEnter(Player player)
    {
      PayRent(player);
    }
    
    private void PayRent(Player player)
    {
      if (_owner != null && _owner.Name != player.Name)
      {
        player.PayMoney(RentToPay);
        _owner.GetMoney(RentToPay);
      }
    }

    public void Buy(Player player)
    {
      if (_owner != null)
        throw new InvalidOperationException("The Street is already owned by Player " + _owner.Name);
        
      player.OwnerShip.Add(this);
      player.PayMoney(Cost.Ground);
      _owner = _game.CurrentPlayer;
    }

    public void LevelUp(Player player, int levels)
    {
      if (_owner == null)
        throw new InvalidOperationException("Nobody ownes this field");
      if (player.Name != _owner.Name)
        throw new ArgumentException("This player can not increase the level ");
      if (levels <= 0 || levels + Level > 5)
        throw new ArgumentException("You cant increase the level by this amount");
      player.PayMoney(Cost.House * levels);
      Level += levels;
    }

    public void TakeMortage(Player player)
    {
      if (player.Name != _owner.Name)
        throw new InvalidOperationException("You cant take mortage on a field that you do not own");
      if (Level > 1)
        throw new InvalidOperationException("You have to sell all your houses and hotels before you can take a mortage on that street");
      if (IsMortage == true)
        throw new InvalidOperationException("You have already token a mortage on that field");
      player.GetMoney(Cost.Mortage);
      IsMortage = true;
    }

    public void PayOffMortage(Player player)
    {
      if (player.Name != _owner.Name)
        throw new InvalidOperationException("You cant pay off the mortage on a field that you do not own");
      if (IsMortage == false)
        throw new InvalidOperationException("You have not took a mortage on that field");
      player.PayMoney(Cost.Mortage + (int)(Cost.Mortage * 0.1));
      IsMortage = false;
    }

    public void SellHouse(Player player,int amount)
    {
      if (player.Name != _owner.Name)
        throw new InvalidOperationException("You do not own this field");
      if (amount >= Level)
        throw new InvalidOperationException("You can not sell this amount of houses");
      Level -= amount;
      player.GetMoney(Cost.House*amount);
    }
  }
}
