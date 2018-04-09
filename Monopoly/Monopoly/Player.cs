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
    public bool Removed { get; private set; }
    private List<IRentableField> _ownerShip = new List<IRentableField>();
    private Game _game;
    public IReadOnlyList<IRentableField> OwnerShip
    {
      get { return _ownerShip; }
    }

    public Player(string name)
    {
      Name = name;
      Money = 1500;
    }

    public void SetGame(Game game)
    {
      _game = game;
    }

    public void AddToOwnerShip(IRentableField streetField)
    {
      _ownerShip.Add(streetField);
    }

    public void RemoveFromOwnerShip(IRentableField fieldToRemove)
    {
      int indexToRemove = 0;
      for (int i = 0; i < OwnerShip.Count(); i++)
      {
        if (fieldToRemove.Name == OwnerShip[i].Name)
          indexToRemove = i;
      }
      _ownerShip.RemoveAt(indexToRemove);
    }

    public void PayMoney(int amount)
    {
      
      if (Money - amount < 0)
      {
        if (_game.IsPlayerBankrupt(this, amount))
          throw new BankruptException();
        else
          throw new NotEnoughMoneyException((amount).ToString());
      }
        
      Money -= amount;
    }

    public void GetMoney(int amount)
    {
      Money += amount;
    }

    public void Remove()
    {
      Removed = true;
    }

  }
}
