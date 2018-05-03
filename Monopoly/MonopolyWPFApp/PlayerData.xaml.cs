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

namespace MonopolyWPFApp
{
  /// <summary>
  /// Interaction logic for PlayerData.xaml
  /// </summary>
  public partial class PlayerData : UserControl //,INotifyPropertyChanged
  {
    public PlayerData()
    {
      InitializeComponent();
      playerPoints = new Ellipse[] { Player1Point, Player2Point, Player3Point, Player4Point };
      DataContext = this;
    }

    //private string _selected;
    //public string SelectedField
    //{
    //  get
    //  {
    //    return _selected;
    //  }
    //  set
    //  {
    //    _selected = value;
    //    OnPropertyChanged("SelectedField");
    //  }
    //}

    public ObservableCollection<string> PlayerOwnerShip { get; set; } = new ObservableCollection<string>();
    public Ellipse[] playerPoints;

    //public event PropertyChangedEventHandler PropertyChanged;
    //private void OnPropertyChanged(String name)
    //{
    //  if (PropertyChanged != null)
    //  {
    //    PropertyChanged(this, new PropertyChangedEventArgs(name));
    //  }
    //}
  }
}
