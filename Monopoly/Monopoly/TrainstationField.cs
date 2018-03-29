using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public class TrainstationField : IRentableField
  {
    public int RentToPay { get; private set; }
    public bool IsMortage { get; private set; }
    public Player Owner { get; private set; }
    public Groups Group { get; private set; }
    public string Name { get; private set; }
    public Costs Cost { get; private set; }
    private Game _game;

    public class Costs
    {
      public int Ground;
      public int[] Rent;
      public int Mortage;
    }

    public TrainstationField(string name, Groups group, Game game, Costs costs)
    {
      Name = name;
      Group = group;
      Cost = costs;
      _game = game;
    }

    public void Buy(Player player)
    {
      if (Owner != null)
        throw new InvalidOperationException("The Street is already owned by Player " + Owner.Name);

      player.AddToOwnerShip(this);
      player.PayMoney(Cost.Ground);
      Owner = _game.CurrentPlayer;
      UpdateRent(player);
    }

    private void UpdateRent(Player player)
    {
      int ownedTrainStations = 0;
      foreach (IRentableField field in _game.RentableFields)
      {
        if(field.GetType() == typeof(TrainstationField) && field.Owner != null && field.Owner.Name == player.Name )
          ownedTrainStations++;
      }
      foreach (IRentableField field in _game.RentableFields)
      {
        if (field.GetType() == typeof(TrainstationField) && field.Owner != null && field.Owner.Name == player.Name)
          ((TrainstationField)field).SetRentToPay(ownedTrainStations);
      }
    }

    public void SetRentToPay(int ownedStations)
    {
      if (ownedStations > 4 || ownedStations < 0)
        throw new ArgumentException("Wrong Amount of Train Stations");
      RentToPay = Cost.Rent[ownedStations - 1];
    }

    public void OnEnter(Player player)
    {
      PayRent(player);
    }

    private void PayRent(Player player)
    {
      if (Owner != null && Owner.Name != player.Name && IsMortage == false)
      {
        player.PayMoney(RentToPay);
        Owner.GetMoney(RentToPay);
      }
    }

    public void TakeMortage(Player player)
    {
      if (Owner == null)
        throw new InvalidOperationException("Nobody ownes this field");
      if (player.Name != Owner.Name)
        throw new InvalidOperationException("You cant take mortage on a field that you do not own");
      if (IsMortage == true)
        throw new InvalidOperationException("You have already token a mortage on that field");
      player.GetMoney(Cost.Mortage);
      IsMortage = true;
    }

    public void PayOffMortage(Player player)
    {
      if (player.Name != Owner.Name)
        throw new InvalidOperationException("You cant pay off the mortage on a field that you do not own");
      if (IsMortage == false)
        throw new InvalidOperationException("You have not took a mortage on that field");
      player.PayMoney(Cost.Mortage + (int)(Cost.Mortage * 0.1));
      IsMortage = false;
    }

    
  }
}
