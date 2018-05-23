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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if((playerNameTextBox.Text.Length != 0))
        PlayerNames.Add(playerNameTextBox.Text);
      playerNameTextBox.Text = null;   
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
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
}
