using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
  public class CardCreator
  {
    public static ICard[] ChanceCards(Game game)
    {
      var cards = new List<ICard>
      {
        new MoneyCard("Bank pays you dividend of $50", 50, game),
        new MoneyCard("Pay poor tay of $15", 15, game),
        new MoneyCard("Your building and loan matures collect $150", 150, game),
        new GoToJailCard("Go directly to jail do not pass go do not collect $200", game),
        new PayPlayersCard("You have been elected chairman of the board pay each player $50", 50, game),
        new GoToNextMemberofGroupCard("Advance token to the nearest railroad and pay owner twice the rental to which he is otherwise entitled. If railroad is unowned, you may buy it from the bank", Groups.TrainStation, game),
        new MoveToCard("Advance to Northumrl'd Avnue", 14, game),
        new MoveToCard("Advance to Go (Collect $200)", 0, game),
        new MoveCard("Go Back 3 Spaces", -3, game),
        new MoveToCard("Take a walk on the Oxford Street advance token to Oxford Street", 32, game),
        new GetOutOfJailCard("This Card may be kept until needed or sold Get Out of Jail", game),
        new GoToNextMemberofGroupCard("Advance token to nearest utility. If unowned you may buy it from bank. If owned throw dice and pay owner a total ten times the amount thrown", Groups.Supplier, game),
        new MoveToCard("Advance to Euston Road. If You Pass Go, Collect $200", 8, game),
        new MoveToCard("Take a Ride to the Vine Street. If you Pass Go collect $200", 19, game),
        new StreetRepairCard("Make Gernal Repairs on all your property. For each House pay $25 for each hotel $100", 25, 100, game)
      };
      return cards.ToArray();
    }

    public static ICard[] ComunityChestCards(Game game)
    {
      var cards = new List<ICard>
      {
        new MoneyCard("Xmas fund matures collect $100", 100, game),
        new MoneyCard("You interhit $100", 100, game),
        new MoneyCard("From sale of stock you get $45", 45, game),
        new MoneyCard("Bank error in your favor collect $200", 200, game),
        new MoneyCard("Pay hospital $100", -100, game),
        new MoneyCard("Doctor's fee pay $50", -50, game),
        new MoneyCard("Receive for services $25", 25, game),
        new MoneyCard("Pay school tax of $150", -150, game),
        new MoneyCard("You have won second prize in a beauty contest collect $10", 10, game),
        new MoneyCard("Income Tax Refund Collect $200", 200, game),
        new MoneyCard("Life insurance matues collect $100", 100, game),
        new GetOutOfJailCard("Get out of jail, free", game),
        new MoveToCard("Advance to Go (Collect $200)", 0, game),
        new GetMoneyFromPlayersCard("Collect $50 from every Player", 50, game),
        new StreetRepairCard("You are assesed for street repairs $40 per House $115 per Hotel", 40, 115, game),
        new GoToJailCard("Go To Jail", game)
      };
      return cards.ToArray();
    }
  }
}
