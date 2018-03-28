using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public interface IField
  {
    string Name { get; }
  
    void OnEnter(IPlayer player);
  }
}
