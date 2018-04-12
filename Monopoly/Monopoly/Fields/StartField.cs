using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  class StartField : IField
  {
    public Groups Group { get; private set; }
    public string Name { get; }
    Game _game;

    public StartField(string name, Game game)
    {
      Name = name;
      _game = game;
    }

   

    public void OnEnter(Player player)
    {
    }
  }
}
