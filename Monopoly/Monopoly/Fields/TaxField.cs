using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Fields
{
  public class TaxField : IField
  {
    public string Name { get; private set; }
    public Groups Group { get; private set; }
    private int _tax;
    private Game _game;

    public TaxField(string name, Groups group, Game game, int tax)
    {
      _tax = tax;
      _game = game;
      Name = name;
      Group = group;
    }

    public void OnEnter(Player player)
    {
      player.PayMoney(_tax);
    }
  }
}
