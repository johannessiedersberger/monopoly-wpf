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
      UpdateField();
    }
    private List<Field> GetAllFields(List<FieldRow> rows)
    {
      return new List<Field>()
      {
        
      };
    }
    public void UpdateField()
    {
      
    }
  }
}
