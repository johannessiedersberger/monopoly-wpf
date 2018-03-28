using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public interface IPlayer
  {
    int Money { get; }

    string Name { get; }

    IReadOnlyList<IRentableField> OwnerShip { get; }

    void AddToOwnerShip(StreetField streetField);

    void PayMoney(int amount);

    void GetMoney(int amount);
  }
}
