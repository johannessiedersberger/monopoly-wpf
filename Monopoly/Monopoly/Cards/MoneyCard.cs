using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class MoneyCard : ICard
  {
    public string Description { get; private set; }
    private int _money;
    private Game _game;

    public MoneyCard(string description, int money, Game game)
    {
      _game = game;
      Description = description;
      _money = money;
    }

    public void UseCard(Player player)
    {
      if (_money > 0)
        player.GetMoney(_money);
      if (_money < 0)
        player.PayMoney(Math.Abs(_money));
    }
  }
}
