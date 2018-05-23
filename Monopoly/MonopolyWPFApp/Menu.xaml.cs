using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
  /// Interaction logic for Menu.xaml
  /// </summary>
  public partial class Menu : UserControl
  {
    public ObservableCollection<string> PlayerNames { get; private set; } = new ObservableCollection<string>();
    private Game _game;
    private MainWindow _mainWindow;

    public Menu(MainWindow mainWindow)
    {
      InitializeComponent();
      _mainWindow = mainWindow;
      playerList.ItemsSource = PlayerNames;
    }

    private void AddPlayerButtonClick(object sender, RoutedEventArgs e)
    {
      if(PlayerNames.Contains(playerNameTextBox.Text))
      {
        MessageBox.Show("You cant use a Player-Name twice");
      }
      else if((playerNameTextBox.Text.Length != 0))
      {
        PlayerNames.Add(playerNameTextBox.Text);
        playerNameTextBox.Text = null;
      }
    }

    private void StartButtonClick(object sender, RoutedEventArgs e)
    {
      if(PlayerNames.Count() <= 1)
      {
        MessageBox.Show("You have to add at least 2 Players to start the game");
      }
      else if(PlayerNames.Count() > 4)
      {
        MessageBox.Show("You can only Play with 4 Players");
      }
      else
      {
        Player[] players = new Player[PlayerNames.Count()];
        for (int i = 0; i < PlayerNames.Count(); i++)
        {
          players[i] = new Player(PlayerNames[i]);
        }
        _game = new Game(players);
        _mainWindow.ShowMonopolyField(_game);
      }     
    }

    private void ClearButtonClick(object sender, RoutedEventArgs e)
    {
      PlayerNames.Clear();
    }
  }
}
