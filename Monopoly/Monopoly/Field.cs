using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public abstract class Field
  {
    public string Name { get; protected set; }

    public abstract void OnEnter(Player player);

  }
}
