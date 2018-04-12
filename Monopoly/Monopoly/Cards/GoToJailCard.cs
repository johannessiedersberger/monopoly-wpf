using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class GoToJailCard : ICard
  {
    public string Description { get; }
    private Game _game;
    
    public GoToJailCard(string description, Game game)
    {
      Description = description;
      _game = game;
    }

    public void UseCard(Player player)
    {
      _game.SetPlayerInPrison(player);
    }
  }
}
