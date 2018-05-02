using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Fields
{
  public class FreeParkingField : IField
  {
    public string Name { get; private set; }

    public Groups Group { get; private set; }

    public FreeParkingField(string name, Groups groups)
    {
      Group = groups;
      Name = name;
    }

    public void OnEnter(Player player)
    {
    }
  }
}
