using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;

namespace Test
{
  class Test
  {
    [Test]
    public void Test1()
    {
      Player[] players = new Player[] { new Player("X"), new Player("Y") };
      Game game = new Game(players);
      for (int i = 0; i < 4; i++)
      {
        game.NextTurn();
      }
      
    }
  }
}
