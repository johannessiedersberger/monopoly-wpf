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
  /// Interaction logic for Field.xaml
  /// </summary>
  public partial class Field : UserControl
  {
    public Field()
    {
      InitializeComponent();
      SetHousesAndHotels(0);
    }

    public void SetHousesAndHotels(int level)
    {
      if(level == 0)
      {
        house1.Visibility = Visibility.Hidden;
        house2.Visibility = Visibility.Hidden;
        house3.Visibility = Visibility.Hidden;
        house4.Visibility = Visibility.Hidden;
        house5.Visibility = Visibility.Hidden;
      }
      if (level == 1)
      {
        house1.Visibility = Visibility.Visible;
        house2.Visibility = Visibility.Hidden;
        house3.Visibility = Visibility.Hidden;
        house4.Visibility = Visibility.Hidden;
        house5.Visibility = Visibility.Hidden;
      }
      if(level == 2)
      {
        house1.Visibility = Visibility.Visible;
        house2.Visibility = Visibility.Visible;
        house3.Visibility = Visibility.Hidden;
        house4.Visibility = Visibility.Hidden;
        house5.Visibility = Visibility.Hidden;
      }
      if (level == 3)
      {
        house1.Visibility = Visibility.Visible;
        house2.Visibility = Visibility.Visible;
        house3.Visibility = Visibility.Visible;
        house4.Visibility = Visibility.Hidden;
        house5.Visibility = Visibility.Hidden;
      }
      if (level == 4)
      {
        house1.Visibility = Visibility.Visible;
        house2.Visibility = Visibility.Visible;
        house3.Visibility = Visibility.Visible;
        house4.Visibility = Visibility.Visible;
        house5.Visibility = Visibility.Hidden;
      }
      if(level == 5)
      {
        house1.Visibility = Visibility.Hidden;
        house2.Visibility = Visibility.Hidden;
        house3.Visibility = Visibility.Hidden;
        house4.Visibility = Visibility.Hidden;
        house5.Visibility = Visibility.Visible;
      }
    }
  }
}
