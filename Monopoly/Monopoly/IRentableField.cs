using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public interface IRentableField : IField
  {
    int RentToPay { get; }
    bool IsMortage { get; }
    Player Owner { get; }
    int MortageValue { get; }

    void SetOwner(Player player);
    void Buy(Player player);
    void TakeMortage(Player player);
    void PayOffMortage(Player player);
    void ExchangeField(Player owner, Player buyer, int negotiatetprice);
    void ExchangeField(Player owner, Player buyer, IRentableField field);
  }
}
