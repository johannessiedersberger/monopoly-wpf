using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;

namespace Monopoly.Fields
{
  public class CommunityChestField : IField
  {
    public string Name { get; private set; }
    public Groups Group { get; private set; }
    private Game _game;
    
    public CommunityChestField(string name, Groups group, Game game)
    {
      _game = game;
      Group = group;
      Name = name;
    }

    public void OnEnter(Player player)
    {
      ICard card = Game.GetCard(_game.CommunityChest.ToArray());
      card.UseCard(player);
      _game.SetLastCardText(player, card.Description);
    }
  }
}
