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
    public void TestNextTurn()
    {
      Game game = new Game(new Player[] {new Player("XXX"), new Player("YYY") });
      game.NextTurn();
      Player[] players = game.Players.ToArray();
      
    }
  }
}
