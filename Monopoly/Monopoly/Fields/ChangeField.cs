using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;
using Monopoly.Fields;

namespace Monopoly.Fields
{
  public class ChangeField : IField
  {
    public string Name { get; private set; }
    public Groups Group { get; private set; }
    private Game _game;
  


    public ChangeField(string name, Groups group, Game game)
    {
      _game = game;
      Group = group;
      Name = name;
    }

    public void OnEnter(Player player)
    {
      ICard card = Game.GetCard(_game.Change.ToArray());
      card.UseCard(player);
    }
  }
}
