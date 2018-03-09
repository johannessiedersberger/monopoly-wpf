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
      Player[] players = _game.Players.ToArray();
    }

    public void Buy(Player player)
    {
      if (_owner != null)
        throw new InvalidOperationException("The Street is already owned by Player " + _owner.Name);
      player.OwnerShip.Add(this);
      player.PayMoney(Cost.Ground);
      _owner = player;
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
