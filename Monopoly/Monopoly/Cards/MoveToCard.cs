using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class MoveToCard : ICard
  {
    public string Description { get; }
    private int _position;
    private Game _game;

    public MoveToCard(string description, int position, Game game)
    {
      Description = description;
      _position = position;
      _game = game;
    }
    public void UseCard(Player player)
    {
      _game.MoveTo(player, _position);
    }
  }
}
