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

    public IReadOnlyDictionary<Player, int> PlayerPos
    {
      get { return _playerPos; }
    }

    public IReadOnlyList<Player> Players
    {
      get { return _players; }
    } 
    
    public Game(Player[] players)
    {
      _players = players;
      _fields = FieldCreator.Create(this);
      foreach (Player player in _players)
        _playerQueue.Enqueue(player);
      foreach (Player player in _players)
        _playerPos.Add(player, 0);
      CurrentPlayer = _playerQueue.First();
    }

    public void NextTurn()
    {
      Player player = _playerQueue.Dequeue();
      _playerQueue.Enqueue(player);
      CurrentPlayer = player;

      _playerPos[player] = ThrowDice(_playerPos[player]);
      _fields[_playerPos[player]].OnEnter(CurrentPlayer);
    }

    private int ThrowDice(int playerPos)
    {
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      int value1 = random.Next(1, 6);
      int value2 = random.Next(1, 6);
      return SetInRange(new int[] { value1, value2 }, playerPos);
    }

    private int SetInRange(int[] diceThrow, int playerPos)
    {
      int diceSum = diceThrow[0] + diceThrow[1]; //TODO: Check for doublets
      if (playerPos + diceSum > _fields.Count()-1)
      {
        CrossedStartField();
        int nextPos = playerPos + diceSum;
        while(nextPos > _fields.Count()-1)
        {
          nextPos -= _fields.Count();
        }
        return nextPos;
      }
      else
      {
        return playerPos + diceSum;
      }
    }

    private void CrossedStartField()
    {
      CurrentPlayer.GetMoney(200);
    }

    public void BuyCurrentField()
    {   
      if(_fields[_playerPos[CurrentPlayer]].GetType() == typeof(StreetField))
      {
        ((StreetField)_fields[_playerPos[CurrentPlayer]]).Buy();
      }     
    }

    public void SetCurrentPlayerPos(int pos)
    {
      _playerPos[CurrentPlayer] = pos;
      _fields[_playerPos[CurrentPlayer]].OnEnter(CurrentPlayer);
    }

    public void SetPlayerPos(int playerIndex, int pos)
    {
      Player player = _players[playerIndex];
      _playerPos[player] = pos;
      _fields[_playerPos[player]].OnEnter(player);
    }

  }
}
