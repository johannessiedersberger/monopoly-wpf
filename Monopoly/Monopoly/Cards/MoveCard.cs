using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class MoveCard : ICard
  {
    public string Description { get; private set; }
    private int _stepsToGo;
    private Game _game;

    public MoveCard(string description, int stepsToGo, Game game)
    {
      Description = description;
      _stepsToGo = stepsToGo;
      _game = game;
    }

    public void UseCard(Player player)
    {
      _game.Move(player, _stepsToGo);
    }
  }
}
