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
    private Game _game;
    private Player _owner;

    public class Costs
    {
      public int Ground;
      public int House;
      public int[] Rent;
      public int Mortage;
    }

    public override void OnEnter()
    {
      Player[] players = _game.Players.ToArray();
    }

    public StreetField(string name, Groups group, Game game, Costs costs)
    {
      Name = name;
      Group = group;
      Cost = costs;
      _game = game;
    }


  }
}
