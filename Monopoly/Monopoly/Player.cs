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
    public List<StreetField> OwnerShip { get; private set; } = new List<StreetField>();

    public Player(string name)
    {
      Name = name;
      Money = 1500;
    }

    private void AddToOwnerShip(StreetField streetField)
    {
      OwnerShip.Add(streetField);
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
