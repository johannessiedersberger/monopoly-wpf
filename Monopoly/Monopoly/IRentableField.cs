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
    
    void Buy(Player player);
    void TakeMortage(Player player);
    void PayOffMortage(Player player);
    void ExchangeField(Player Owner, Player Buyer);
  }
}
