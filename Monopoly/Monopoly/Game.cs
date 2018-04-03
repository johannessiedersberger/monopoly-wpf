using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  public class Game
  {
    private IField[] _fields;
    private Player[] _players;
    private Dictionary<Player, int> _playerPos = new Dictionary<Player, int>();
    private Dictionary<Player, List<int[]>> _diceThrows = new Dictionary<Player, List<int[]>>();
    private Queue<Player> _playerQueue = new Queue<Player>();
    private IEnumerable<IField> _rentableFields;  

    public Player CurrentPlayer { get; private set; }

    public IReadOnlyDictionary<Player, int> PlayerPos
    {
      get { return _playerPos; }
    }

    public IReadOnlyList<Player> Players
    {
      get { return _players; }
    }

    public IReadOnlyList<IField> Fields
    {
      get { return _fields; }
    }

    public IReadOnlyList<IField> RentableFields
    {
      get { return _rentableFields.ToList(); }
    }

    public IReadOnlyDictionary<Player, List<int[]>> DiceThrows
    {
      get { return _diceThrows; }
    }

    public Game(Player[] players)
    {
      _players = players;
      _fields = FieldCreator.Create(this);
      _rentableFields = _fields.Where(i => i is IRentableField);
      foreach (Player player in _players)
        _playerQueue.Enqueue(player);
      foreach (Player player in _players)
        _playerPos.Add(player, 0);
      foreach (Player player in _players)
        _diceThrows.Add(player, new List<int[]>());
      CurrentPlayer = _playerQueue.First();      
    }

    public void NextTurn()
    {
      Player player = _playerQueue.Dequeue();
      _playerQueue.Enqueue(player);
      CurrentPlayer = player;

      int[] dices = ThrowDice(player);
      SaveDiceThrow(player, dices);
      
      _playerPos[player] = SetInRange(dices, PlayerPos[player]);
      _fields[_playerPos[player]].OnEnter(CurrentPlayer);
    }

    private int[] ThrowDice(Player player)
    {
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      int value1 = random.Next(1, 6);
      int value2 = random.Next(1, 6);
      int[] dices = new int[] { value1, value2 };
      return dices;
    }

    private void SaveDiceThrow(Player player ,int[] dices)
    {
      _diceThrows[player].Add(dices);
      if (_diceThrows[player].Count() >= 3)
        _diceThrows[player].RemoveAt(0);    
    }

    private int SetInRange(int[] diceThrow, int playerPos)
    {
      int diceSum = diceThrow[0] + diceThrow[1]; //TODO: Check for doublets
      if (playerPos + diceSum > _fields.Count() - 1)
      {
        CrossedStartField();
        int nextPos = playerPos + diceSum;
        while (nextPos > _fields.Count() - 1)
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

    public int CheckForDoublets(Player player)
    {
      int doublets = 0;
      bool lastCheck = false;
      for (int i = _diceThrows[player].Count() - 1; i >= 0; i--)
      {
        if (_diceThrows[player][i][0] == _diceThrows[player][i][1])
        {
          if (lastCheck)
            doublets++;
          lastCheck = true;
        }
        else
        {
          lastCheck = false;
        }
      }
      return doublets;
    }

    public void BuyCurrentStreet(Player player)
    {
      IField field = _fields[_playerPos[player]];
      if (field.GetType() != typeof(StreetField))
        throw new InvalidOperationException("You are not on a street field");
      ((StreetField)field).Buy(player);

    }

    public void SetPlayerPos(Player player, int pos)
    {
      _playerPos[player] = pos;
      _fields[_playerPos[player]].OnEnter(player);
    }

    public bool DoesPlayerOwnCompleteGroup(Player player, Groups group)
    {
      foreach (IRentableField currentField in _rentableFields)
      {
        if (currentField.Group == group)
        {
          if (currentField.Owner == null)
            return false;
          else
            if (currentField.Owner.Name != player.Name)
            return false;
        }
      }
      return true;
    }

    public int NumberOfPropertiesOfGroupOwned(Player player, Groups group)
    {
      int numberOfProperties = 0;
      foreach(IRentableField currentField in _rentableFields)
      {
        if(currentField.Group == group)
        {
          if (currentField.Owner != null && currentField.Owner.Name == player.Name)
            numberOfProperties++;
        }
      }
      return numberOfProperties;
    }
  }
}

