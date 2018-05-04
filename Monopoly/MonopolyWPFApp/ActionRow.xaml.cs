using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Monopoly;

namespace MonopolyWPFApp
{
  /// <summary>
  /// Interaction logic for ActionRow.xaml
  /// </summary>
  public partial class ActionRow : UserControl
  {
    private Game _game;
    private MonopolyField _monopolyField;

    public ActionRow()
    {
      InitializeComponent();
      DataContext = this;
    }

    public void SetGame(Game game)
    {
      _game = game;
    }
    public void SetMonopolyField(MonopolyField monopolyField)
    {
      _monopolyField = monopolyField;
    }

    private int cubeThrows;
    private void ThrowCubesButton(object sender, RoutedEventArgs e)
    {
      if (cubeThrows >= 1)
      {
        MessageBox.Show("You can not throw again");
      }
      else
      {
        _game.GoForward(_game.CurrentPlayer);
        _monopolyField.Update();
        PayMessageBox();
        cubeThrows++;
      }
    }

    private void FinishTurnButton(object sender, RoutedEventArgs e)
    {
      if (cubeThrows < 1)
      {
        MessageBox.Show("You have to Throw the Cubes");
      }
      else
      {
        _game.NextPlayer();
        _monopolyField.Update();
        ResetVariables();
        ResetFieldDataPreview();
        
      }
    }

    

    private bool IsRentableField(IField currentField)
    {
      return currentField.GetType() == typeof(StreetField)
          || currentField.GetType() == typeof(TrainstationField)
          || currentField.GetType() == typeof(SupplierField);
    }

    #region 

    //private bool CheckForAuction()
    //{
    //  if (_game.CurrentPlayer.Removed == false) // Auction
    //  {
    //    IField currentField = _game.Fields[_game.PlayerPos[_game.CurrentPlayer]];
    //    if (IsRentableField(currentField) && ((IRentableField)currentField).Owner == null)
    //    {

    //      return true;
    //    }
    //  }
    //  return false;
    //}

    //private bool IsAuctionRunning = false;
    //private void AskForAuction()
    //{
    //  IField currentField = _game.Fields[_game.PlayerPos[_game.CurrentPlayer]];
    //  var result = MessageBox.Show("Is Someone interested in the " + currentField.Name, "Auction", MessageBoxButton.YesNo, MessageBoxImage.Question);

    //  if (result == MessageBoxResult.Yes)
    //  {
    //    Auction(currentField);
    //  }
    //}

    //private void Auction(IField currentField)
    //{
    //  SetPlayerAuctionControls();
    //}

    //private void SetPlayerAuctionControls()
    //{
    //  auctionGrid.Visibility = Visibility.Visible;
    //  if (_game.Players.Count() >= 1)
    //  {
    //    player1bidNameLabel.Visibility = Visibility.Visible;
    //    player1bid.Visibility = Visibility.Visible;
    //  }
    //  else
    //  {
    //    player1bidNameLabel.Visibility = Visibility.Hidden;
    //    player1bid.Visibility = Visibility.Hidden;
    //  }
    //  if (_game.Players.Count() >= 2)
    //  {
    //    player2bidNameLabel.Visibility = Visibility.Visible;
    //    player2bid.Visibility = Visibility.Visible;

    //  }
    //  else
    //  {
    //    player2bidNameLabel.Visibility = Visibility.Hidden;
    //    player2bid.Visibility = Visibility.Hidden;
    //  }
    //  if (_game.Players.Count() >= 3)
    //  {
    //    player3bidNameLabel.Visibility = Visibility.Visible;
    //    player3bid.Visibility = Visibility.Visible;
    //  }
    //  else
    //  {
    //    player3bidNameLabel.Visibility = Visibility.Hidden;
    //    player3bid.Visibility = Visibility.Hidden;
    //  }
    //  if (_game.Players.Count() >= 4)
    //  {
    //    player4bidNameLabel.Visibility = Visibility.Visible;
    //    player4bid.Visibility = Visibility.Visible;
    //  }
    //  else
    //  {
    //    player4bidNameLabel.Visibility = Visibility.Hidden;
    //    player4bid.Visibility = Visibility.Hidden;
    //  }
    //}

    //private void FinishAuctionButton(object sender, RoutedEventArgs e)
    //{
    //  IField currentField = _game.Fields[_game.PlayerPos[_game.CurrentPlayer]];
    //  _game.StartAuction(new List<IRentableField> { (IRentableField)currentField });

    //  ////Auction(_game);
    //  //_game.FinishAuction();
    //}

    //private bool CheckBids()
    //{
    //  try
    //  {
    //    List<int> bids = new List<int>();
    //    TextBox[] playerBids = GetBidTextBoxes();
    //    foreach(TextBox box in playerBids)
    //    {
    //      if(box.Text == string.Empty || box.Text== "")
    //      {
    //        box.Text = "0";
    //      }
    //      else
    //      {
    //        bids.Add(int.Parse(box.Text));
    //      }
    //    }

    //    foreach(int currentBid in bids)
    //    {
    //      currentBid = 
    //    }
    //  }
    //  catch
    //  {
    //    MessageBox.Show("Wrong Input");
    //  }
    //}
    //private TextBox[] GetBidTextBoxes()
    //{
    //  return new TextBox[] { player1bid, player2bid, player3bid, player4bid };
    //}
    #endregion


    private void ResetVariables()
    {
      cubeThrows = 0;
      selectedField = null;
    }

    private void PayMessageBox()
    {
      if (_game.LastPayMent.Count() != 0)
      {
        MessageBox.Show("You have to pay " + _game.LastPayMent[_game.CurrentPlayer] + "$");
        _game.ClearLastPayment();
      }
    }

    private void BuyButton(object sender, RoutedEventArgs e)
    {
      IField field = _game.Fields[_game.PlayerPos[_game.CurrentPlayer]];

      try
      {
        ((IRentableField)field).Buy(_game.CurrentPlayer);
        _monopolyField.Update();
      }
      catch
      {
        MessageBox.Show("You cant buy this Field");
      }
      PayMessageBox();
    }

    private void LevelUpButton(object sender, RoutedEventArgs e)
    {
      try
      {
        ((StreetField)selectedField).LevelUp(_game.CurrentPlayer, int.Parse(inputField.Text));
        _monopolyField.Update();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private IField selectedField;
    private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
      var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
      if (item != null)
      {
        // ListBox item clicked - do some cool things here
        selectedField = GetField(item.Content.ToString());
        SetFieldDataPreview();
      }
    }

    private IField GetField(string name)
    {
      foreach (IRentableField field in _game.RentableFields)
      {
        if (field.Name == name)
        {
          return field;
        }
      }
      return null;
    }

    public void SetFieldDataPreview()
    {
      if (selectedField == null)
        return;
      groupLabel.Content = selectedField.Group.ToString();
      nameLabel.Content = selectedField.Name;
      mortageValueLabel.Content = "Mortage Value " + ((IRentableField)selectedField).MortageValue + "$";
      isMortageLabel.Content = "Mortage Taken: " + ((IRentableField)selectedField).IsMortage;

      if (selectedField.GetType() == typeof(StreetField))
      {
        levelLabel.Content = "Level " + ((StreetField)selectedField).Level;
        pricePerHouseLabel.Content = "Price Per House " + ((StreetField)selectedField).Cost.House + "$";
        int[] rent = ((StreetField)selectedField).Cost.Rent;
        rentCosts.Content = "Rent: " + rent[0] + "$ " + rent[1] + "$ " + rent[2] + "$ " + rent[3] + "$ " + rent[4] + "$ " + rent[5] + "$";
        rentToPay.Content = "Rent To Pay: " + ((IRentableField)selectedField).RentToPay + "$";
      }
      else if (selectedField.GetType() == typeof(TrainstationField))
      {
        rentCosts.Content = "Rent: 25$/50$/100$/200$";
        levelLabel.Content = null;
        pricePerHouseLabel.Content = null;
        rentToPay.Content = "Rent To Pay: " + ((IRentableField)selectedField).RentToPay + "$";
      }
      else if (selectedField.GetType() == typeof(SupplierField))
      {
        rentCosts.Content = "Dice Throw Multiplied with 4/10";
        levelLabel.Content = null;
        pricePerHouseLabel.Content = null;
        rentToPay.Content = "Rent Depends on Dice Throw";

      }
      else
      {
        levelLabel.Content = null;
        pricePerHouseLabel.Content = null;
        rentCosts.Content = null;
      }
    }

    private void ResetFieldDataPreview()
    {
      groupLabel.Content = null;
      nameLabel.Content = null;
      mortageValueLabel.Content = null;
      isMortageLabel.Content = null;
      levelLabel.Content = null;
      pricePerHouseLabel.Content = null;
      rentCosts.Content = null;
    }

    private void MortageButton(object sender, RoutedEventArgs e)
    {
      try
      {
        IRentableField field = (IRentableField)selectedField;
        if ((field).IsMortage == false)
          (field).TakeMortage(_game.CurrentPlayer);
        else
          (field).PayOffMortage(_game.CurrentPlayer);
        _monopolyField.Update();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void SellHouseButton(object sender, RoutedEventArgs e)
    {
      try
      {
        StreetField field = (StreetField)selectedField;
        field.SellHouse(_game.CurrentPlayer, int.Parse(inputField.Text));
        _monopolyField.Update();
      }
      catch
      {
        MessageBox.Show("You cant sell houses on that field");
      }
    }

    private void ExchangeFieldButton(object sender, RoutedEventArgs e)
    {

    }

    
  }
}
