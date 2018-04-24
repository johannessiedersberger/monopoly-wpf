using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class PayPlayersCard : ICard
  {
    public string Description { get; private set; }
    private int _money;
    private Game _game; 

    public PayPlayersCard(string description, int money, Game game)
    {
      Description = description;
      _money = money;
      _game = game;
    }

    public void UseCard(Player player)
    {
      foreach(Player otherPlayers in _game.Players)
      {
        if(otherPlayers.Name != player.Name)
        {
          otherPlayers.GetMoney(_money);
        }
      }
      player.PayMoney((_game.Players.Count() - 1) * _money);
    }
  }
}
