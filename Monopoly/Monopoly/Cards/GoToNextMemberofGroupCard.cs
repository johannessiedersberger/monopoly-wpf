using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class GoToNextMemberofGroupCard : ICard
  {
    public string Description { get; private set; }
    private Groups _group;
    private Game _game;

    public GoToNextMemberofGroupCard(string description, Groups group, Game game)
    {
      Description = description;
      _group = group;
      _game = game;
    }

    public void UseCard(Player player)
    {
      int playerPos = _game.PlayerPos[player];
      
      //After Player
      for (int currentPosition = playerPos; currentPosition <= _game.Fields.Count()-1; currentPosition++)
      {
        if(_game.Fields[currentPosition].Group == _group)
        {
          _game.SetPlayerPos(player, currentPosition);
          return;
        }
      }
      //Before Player
      for (int currentPosition = 0; currentPosition < playerPos; currentPosition++)
      {
        if (_game.Fields[currentPosition].Group == _group)
        {
          _game.SetPlayerPos(player, currentPosition);
          return;
        }
      }

    }
  }
}
