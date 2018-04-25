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

namespace MonopolyWPFApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public Menu _menu { get; }
    public MonopolyField _monopolyField { get; }

    public MainWindow()
    {
      InitializeComponent();
      _menu = new Menu(this);
      _monopolyField = new MonopolyField();
      contentControl.Content = _menu;
    }

    public void ShowMonopolyField()
    {
      contentControl.Content = _monopolyField;
    }
  }
}
