using System;
using System.Collections.Generic;
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
using Monopoly.Fields;
namespace MonopolyWPFApp
{
  /// <summary>
  /// Interaction logic for monopolyField.xaml
  /// </summary>
  public partial class MonopolyField : UserControl
  {
    private Game _game;
    private List<Field> _fields;
    public MonopolyField(Game game)
    {
      InitializeComponent();
      _fields = GetAllFields();
      _game = game;
      Update();


    }

    private List<Field> GetAllFields()
    {
      var fields = new List<List<Field>>()
      {
        GetFields(row0),
        GetFields(row1),
        GetFields(row2),
        GetFields(row3),
      }.SelectMany(i => i).Distinct();
      return fields.ToList();
    }

    private List<Field> GetFields(FieldRow row)
    {
      return new List<Field>()
      {
        row.feld1,
        row.feld2,
        row.feld3,
        row.feld4,
        row.feld5,
        row.feld6,
        row.feld7,
        row.feld8,
        row.feld9
      };
    }

    public void Update()
    {
      for (int i = 0; i < _game.Fields.Count(); i++)
      {

        if (i == 0)//StartField
        {
          gofield.fieldNameLabel.Content = _game.Fields[0].Name;
          gofield.arrow.Visibility = Visibility.Visible;
          SetPlayersOnBigField(gofield, i);
        }
        else if (i == 10)//Prison
        {
          prison.fieldNameLabel.Content = _game.Fields[10].Name;
          prison.prison.Visibility = Visibility.Visible;
          SetPlayersOnBigField(prison, i);
        }
        else if (i == 20)//Free Parking
        {
          freeParking.fieldNameLabel.Content = _game.Fields[20].Name;
          freeParking.car.Visibility = Visibility.Visible;
          SetPlayersOnBigField(freeParking, i);
        }
        else if (i == 30)//Go To Jail
        {
          goToJail.fieldNameLabel.Content = _game.Fields[30].Name;
          goToJail.policeMan.Visibility = Visibility.Visible;
          SetPlayersOnBigField(goToJail, i);
        }
        else if (i < 10)
        {
          SetFieldData(i, -1);
        }
        else if (i > 10 && i < 20)
        {
          SetFieldData(i, -2);
        }
        else if (i > 20 && i < 30)
        {
          SetFieldData(i, -3);
        }
        else if (i > 30 && i < 40)
        {
          SetFieldData(i, -4);
        }
      }

      SetPlayerData();
    }

    private void SetPlayerData()
    {
      PlayerData[] playerDatas = GetPlayerDatas();
      for (int i = 0; i < playerDatas.Length; i++)
      {
        playerDatas[i].Visibility = Visibility.Visible;
        playerDatas[i].playerName.Content = _game.Players[i].Name;
        playerDatas[i].playerPoints[i].Visibility = Visibility.Visible;
        playerDatas[i].playerMoney.Content = _game.Players[i].Money;
        SetPlayerOwnerShip(playerDatas);
        playerDatas[i].playerOwnerShip.ItemsSource = playerDatas[i].PlayerOwnerShip;
        if (_game.Players[i].Name == _game.CurrentPlayer.Name)
          playerDatas[i].playerName.Background = new SolidColorBrush(Colors.Yellow);
        else
          playerDatas[i].playerName.Background = new SolidColorBrush(Colors.Wheat);
      }
    }

    private PlayerData[] GetPlayerDatas()
    {
      PlayerData[] datas = new PlayerData[] { playerDataRow.Player1Data, playerDataRow.Player2Data, playerDataRow.Player3Data, playerDataRow.Player4Data };
      Array.Resize(ref datas, _game.Players.Count());
      return datas;
    }

    private void SetPlayerOwnerShip(PlayerData[] playerDatas)
    {
      for (int data = 0; data < playerDatas.Length; data++)
      {
        playerDatas[data].PlayerOwnerShip.Clear();///!!!!!!!
        for (int ownership = 0; ownership < _game.Players[data].OwnerShip.Count(); ownership++)
        {
          playerDatas[data].PlayerOwnerShip.Add(_game.Players[data].OwnerShip[ownership].Name);
        }
      }
    }

    private void SetFieldData(int position, int c)
    {
      _fields[position + c].fieldNameLabel.Content = _game.Fields[position].Name;
      if (_game.Fields[position].GetType() == typeof(StreetField))
      {
        _fields[position + c].SetHousesAndHotels(((StreetField)_game.Fields[position]).Level);
        _fields[position + c].fieldColorRectangle.Fill = GetColorBrush(((StreetField)_game.Fields[position]).Group);
      }
      else
      {
        _fields[position + c].fieldColorRectangle.Fill = null;
      }
      if (_game.Fields[position].GetType() == typeof(TrainstationField))
      {
        _fields[position + c].Train.Visibility = Visibility.Visible;
      }
      if (_game.Fields[position].GetType() == typeof(ChangeField))
      {
        _fields[position + c].QuestionMark.Visibility = Visibility.Visible;
      }
      if (_game.Fields[position].GetType() == typeof(CommunityChestField))
      {
        _fields[position + c].Chest.Visibility = Visibility.Visible;
      }
      if (_game.Fields[position].GetType() == typeof(SupplierField))
      {
        if (_game.Fields[position].Name == FieldNames.WaterWorks)
          _fields[position + c].Water.Visibility = Visibility.Visible;
        else if (_game.Fields[position].Name == FieldNames.EnergyCompany)
          _fields[position + c].Bulb.Visibility = Visibility.Visible;
      }

      if (_game.Fields[position].GetType() == typeof(StreetField) || _game.Fields[position].GetType() == typeof(TrainstationField)
      || _game.Fields[position].GetType() == typeof(SupplierField))
        _fields[position + c].fieldPriceLabel.Content = (((IRentableField)_game.Fields[position]).MortageValue * 2).ToString() + " $";
      else
        _fields[position + c].fieldPriceLabel.Content = null;
      SetPlayers(position, c);

    }
    private SolidColorBrush GetColorBrush(Groups groups)
    {
      if (groups == Groups.Brown)
        return new SolidColorBrush(Colors.Brown);
      if (groups == Groups.DarkBlue)
        return new SolidColorBrush(Colors.DarkBlue);
      if (groups == Groups.Green)
        return new SolidColorBrush(Colors.Green);
      if (groups == Groups.LightBlue)
        return new SolidColorBrush(Colors.LightBlue);
      if (groups == Groups.Orange)
        return new SolidColorBrush(Colors.Orange);
      if (groups == Groups.Pink)
        return new SolidColorBrush(Colors.Pink);
      if (groups == Groups.Red)
        return new SolidColorBrush(Colors.Red);
      if (groups == Groups.Yellow)
        return new SolidColorBrush(Colors.Yellow);
      throw new ArgumentException();
    }
    private void SetPlayers(int position, int c)
    {
      if (_game.PlayerPos.Count() >= 1 && _game.PlayerPos[_game.Players[0]] == position)
      {
        _fields[position + c].Player1.Visibility = Visibility.Visible;
      }
      else
      {
        _fields[position + c].Player1.Visibility = Visibility.Hidden;
      }

      if (_game.PlayerPos.Count() >= 2 && _game.PlayerPos[_game.Players[1]] == position)
      {
        _fields[position + c].Player2.Visibility = Visibility.Visible;
      }
      else
      {
        _fields[position + c].Player2.Visibility = Visibility.Hidden;
      }

      if (_game.PlayerPos.Count() >= 3 && _game.PlayerPos[_game.Players[2]] == position)
      {
        _fields[position + c].Player3.Visibility = Visibility.Visible;
      }
      else
      {
        _fields[position + c].Player3.Visibility = Visibility.Hidden;
      }

      if (_game.PlayerPos.Count() >= 4 && _game.PlayerPos[_game.Players[3]] == position)
      {
        _fields[position + c].Player4.Visibility = Visibility.Visible;
      }
      else
      {
        _fields[position + c].Player4.Visibility = Visibility.Hidden;
      }
    }

    private void SetPlayersOnBigField(BigField bigField, int position)
    {
      if (_game.PlayerPos.Count() >= 1 && _game.PlayerPos[_game.Players[0]] == position)
      {
        bigField.Player1.Visibility = Visibility.Visible;
      }
      else
      {
        bigField.Player1.Visibility = Visibility.Hidden;
      }

      if (_game.PlayerPos.Count() >= 2 && _game.PlayerPos[_game.Players[1]] == position)
      {
        bigField.Player2.Visibility = Visibility.Visible;
      }
      else
      {
        bigField.Player2.Visibility = Visibility.Hidden;
      }

      if (_game.PlayerPos.Count() >= 3 && _game.PlayerPos[_game.Players[2]] == position)
      {
        bigField.Player3.Visibility = Visibility.Visible;
      }
      else
      {
        bigField.Player3.Visibility = Visibility.Hidden;
      }

      if (_game.PlayerPos.Count() >= 4 && _game.PlayerPos[_game.Players[3]] == position)
      {
        bigField.Player4.Visibility = Visibility.Visible;
      }
      else
      {
        bigField.Player4.Visibility = Visibility.Hidden;
      }
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
        Update();
        PayMessageBox();
        cubeThrows++;
      }   
    }

    private void FinishTurnButton(object sender, RoutedEventArgs e)
    {
      if(cubeThrows < 1)
      {
        MessageBox.Show("You have to Throw the Cubes");
      }
      else
      {
        _game.NextPlayer();
        Update();
        cubeThrows = 0;
      }      
    }

    private void BuyButton(object sender, RoutedEventArgs e)
    {
      IField field = _game.Fields[_game.PlayerPos[_game.CurrentPlayer]];
     
      try
      {
        ((IRentableField)field).Buy(_game.CurrentPlayer);
        Update();
      }
      catch(Exception ex)
      {
        MessageBox.Show("You cant buy this Field");
      }
      PayMessageBox();
    }

    private void LevelUpButton(object sender, RoutedEventArgs e)
    {
      
    }


    
    private void PayMessageBox()
    {
      if (_game.LastPayMent.Count() != 0)
      {
        MessageBox.Show("You have to pay " + _game.LastPayMent[_game.CurrentPlayer] + "$");
        _game.ClearLastPayment();
      }
    }
  }
}