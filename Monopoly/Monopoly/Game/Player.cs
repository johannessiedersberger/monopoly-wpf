using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;

namespace Monopoly
{
  public class Player 
  {
    public int Money { get; private set; }
    public string Name { get; private set; }
    public bool Removed { get; private set; }
    public bool InPrison { get; set; }
    private List<IRentableField> _ownerShip = new List<IRentableField>();
    private List<ICard> _card = new List<ICard>();
    private Game _game;

    public IReadOnlyList<IRentableField> OwnerShip
    {
      get { return _ownerShip; }
    }

    public IReadOnlyList<ICard> Cards
    {
      get { return _card; }
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
      _game.SetLastPayMent(this,amount);
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

    public void AddCard(ICard cardToAdd)
    {
      _card.Add(cardToAdd);
    }

    public void RemoveGoToJailCard()
    {
      foreach(ICard card in _card)
      {
        if (card.GetType() == typeof(GetOutOfJailCard))
          _card.Remove(card);
      }
    }

  }
}
