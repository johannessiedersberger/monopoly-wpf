using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class GetOutOfJailCard : ICard
  {
    public string Description { get; private set; }
    private Game _game;

    public GetOutOfJailCard(string description, Game game)
    {
      Description = description;
      _game = game;
    }

    public void UseCard(Player player)
    {
      if (player.Cards.Contains(this))
      {      
        _game.RemovePlayerFromPrison(player);
        player.RemoveCard(this);
      }
      else
      {
        player.AddCard(this);
      }
      
    }
  }
}
