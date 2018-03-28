using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public class Player
  {
    public int Money { get; private set; }
    public string Name { get; private set; }
    private List<StreetField> _ownerShip  = new List<StreetField>();
    public IReadOnlyList<StreetField> OwnerShip
    {
      get { return _ownerShip; }
    }

    public Player(string name)
    {
      Name = name;
      Money = 1500;
    }

    public void AddToOwnerShip(StreetField streetField)
    {
      _ownerShip.Add(streetField);
    }

    public void PayMoney(int amount)
    {
      Money -= amount;
    }

    public void GetMoney(int amount)
    {
      Money += amount;
    }

  }
}
