using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public class Game
  {
    private Field[] _fields;
    private Player[] _players;
    private Dictionary<Player, int> _playerPos = new Dictionary<Player, int>();
    private Queue<Player> _playerQueue = new Queue<Player>();

    public Player CurrentPlayer { get; private set; }

    public IReadOnlyCollection<Player> Players
    {
      get
      {
        return Array.AsReadOnly(_players);
      }
    }

    public Game(Player[] players)
    {
      _players = players;
      _fields = FieldCreator.Create(this);

      foreach (Player player in _players)
        _playerQueue.Enqueue(player);
      foreach (Player player in _players)
        _playerPos.Add(player, 0);
    }

    public void NextTurn()
    {
      Player player = _playerQueue.Dequeue();
      _playerQueue.Enqueue(player);
      CurrentPlayer = player;

      _playerPos[player] = ThrowDice(_playerPos[player]);
      _fields[_playerPos[player]].OnEnter();

    }

    private int ThrowDice(int playerPos)
    {
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      return SetInRange(random.Next(1, 12), playerPos);
    }

    private int SetInRange(int diceThrow, int playerPos)
    {
      if (playerPos + diceThrow > _fields.Count())
      {
        int nextPos = playerPos + diceThrow;
        while(nextPos > _fields.Count())
        {
          nextPos -= _fields.Count();
        }
        return nextPos;
      }
      else
      {
        return playerPos + diceThrow;
      }
        
    }


  }
 
}
