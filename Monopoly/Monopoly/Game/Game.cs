using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;

namespace Monopoly
{
  public class Game
  {
    private IField[] _fields;
    private ICard[] _changeCard;
    private ICard[] _communityChestCards;
    private List<Player> _players;
    private Dictionary<Player, int> _playerPositions = new Dictionary<Player, int>();
    private Dictionary<Player, List<int[]>> _diceThrows = new Dictionary<Player, List<int[]>>();
    private Dictionary<Player, int> _lastPayMent = new Dictionary<Player, int>();
    private Dictionary<Player, int> _triesToEscapeFromPrison = new Dictionary<Player, int>();
    private Dictionary<Player, String> _lastCardText = new Dictionary<Player, string>();
    private Queue<Player> _playerQueue = new Queue<Player>();
    private IEnumerable<IField> _rentableFields;
    

    public Player CurrentPlayer { get; private set; }

    public IReadOnlyDictionary<Player, int> PlayerPos
    {
      get { return _playerPositions; }
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

    public IReadOnlyList<int> GetLastThrow(Player player)
    {
      return Array.AsReadOnly(_diceThrows[player][_diceThrows[player].Count() - 1]);
    }

    public IReadOnlyDictionary<Player,int> LastPayMent
    {
      get { return _lastPayMent; }
    }

    public IReadOnlyList<ICard> CommunityChest
    {
      get { return _communityChestCards; }
    }

    public IReadOnlyList<ICard> Change
    {
      get { return _changeCard; }
    }

    public IReadOnlyDictionary<Player, string> LastCardText
    {
      get { return _lastCardText; }
    }

    public void SetLastCardText(Player player, string cardText)
    {
      _lastCardText.Add(player, cardText);
    }

    public Game(Player[] players)
    {
      _players = players.ToList();
      _fields = FieldCreator.Create(this);
      _rentableFields = _fields.Where(i => i is IRentableField);
      _changeCard = CardCreator.ChanceCards(this);
      _communityChestCards = CardCreator.ComunityChestCards(this);
      
      foreach (Player player in _players)
        _playerQueue.Enqueue(player);
      foreach (Player player in _players)
        _playerPositions.Add(player, 0);
      foreach (Player player in _players)
        _diceThrows.Add(player, new List<int[]>());
      foreach (Player player in _players)
        player.SetGame(this);
      foreach (Player player in _players)
        _triesToEscapeFromPrison.Add(player, 0);
      
      NextPlayer();//TEST
    }

    public void NextPlayer()
    {
      Player player = _playerQueue.Dequeue();

      CurrentPlayer = player;

      _playerQueue.Enqueue(player);
      _lastPayMent.Clear();
      _lastCardText.Clear();
    }

    
    public void GoForward(Player player, int[] diceThrow)
    {
      SaveDiceThrow(player, diceThrow);
      SetPlayerPosition(player, diceThrow);     
    }

    public void GoForward(Player player)
    {
      SetPlayerPosition(player, ThrowDice(player));
    }

    private int[] ThrowDice(Player player)
    {
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      int value1 = random.Next(1, 6);
      int value2 = random.Next(1, 6);
      int[] dices = new int[] { value1, value2 };
      SaveDiceThrow(player, dices);
     
      return dices;
    }


    private void SetPlayerPosition(Player player, int[] dices)
    {
      if (Double(player, dices) && CheckForDoubletsInARow(player) >= 2)
      {
        SetPlayerInPrison(player);
        return;
      }

      if (CheckForPrison(player, dices) == false)
        return;

      _playerPositions[player] = SetInRange(dices, PlayerPos[player]);

      _fields[_playerPositions[player]].OnEnter(player);
    }

    private void SetPlayerPosition(Player player, int position)
    {
      _playerPositions[player] = position;
      _fields[_playerPositions[player]].OnEnter(player);
    }
    
    private bool CheckForPrison(Player player, int[] dices)
    {     
      if (player.InPrison == false)     
        return true;      
      else      
        _triesToEscapeFromPrison[player]++;
      
      if (Double(player, dices) == false && _triesToEscapeFromPrison[player] >= 3)// if inPrison
      {
        PayFineImmediately(player);
        return true;
      }     
      else if (player.InPrison && Double(player, dices))
      {
        RemovePlayerFromPrison(player);
        return true;
      }
      else
      {
        return false;
      }      
    }
    
    public void PayFineImmediately(Player player)
    {     
      RemovePlayerFromPrison(player);
      player.PayMoney(50);
      GoForward(player);
    }

    public void RemovePlayerFromPrison(Player player)
    {
      if (player.InPrison == false)
        throw new InvalidOperationException("The player is not in Jail and has not to be removed");
      player.InPrison = false;
      _triesToEscapeFromPrison[player] = 0;
    }

    public void SetPlayerInPrison(Player player)
    {
      _playerPositions[player] = 10;
      player.InPrison = true;
    }
    
    public void CallOnEnter(Player player)
    {
      _fields[_playerPositions[player]].OnEnter(player);
    }

    private void SaveDiceThrow(Player player, int[] dices)
    {
      _diceThrows[player].Add(dices);
      if (_diceThrows[player].Count() > 3)
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

    public int CheckForDoubletsInARow(Player player)
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

    public bool Double(Player player, int[] diceThrow)
    {
      return diceThrow[0] == diceThrow[1];
    }

    public void BuyCurrentStreet(Player player)
    {
      IField field = _fields[_playerPositions[player]];
      if (field.GetType() != typeof(StreetField))
        throw new InvalidOperationException("You are not on a street field");
      ((StreetField)field).Buy(player);

    }

    public void SetPlayerPos(Player player, int pos)
    {
      _playerPositions[player] = pos;
      _fields[_playerPositions[player]].OnEnter(player);
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
      foreach (IRentableField currentField in _rentableFields)
      {
        if (currentField.Group == group)
        {
          if (currentField.Owner != null && currentField.Owner.Name == player.Name)
            numberOfProperties++;
        }
      }
      return numberOfProperties;
    }

    public void SetLastThrow(Player player, List<int[]> throwed)
    {
      _diceThrows[player] = throwed;
    }

    public void SetLastPayMent(Player player, int amount)
    {
      if (LastPayMent.ContainsKey(player) == false)
        _lastPayMent.Add(player, amount);
      else
        _lastPayMent[player] = amount;
    }

    public void ClearLastPayment()
    {
      _lastPayMent.Clear();
    }

    public bool IsPlayerBankrupt(Player player, int neededAmount)
    {
      return player.Money - neededAmount + GetSavings(player) < 0;
    }

    private static int GetSavings(Player player)
    {
      int savings = 0;
      foreach (IRentableField field in player.OwnerShip)
      {
        if (field.GetType() == typeof(StreetField))
        {
          StreetField street = ((StreetField)field);
          savings += street.Level * street.Cost.House;
        }
        if (field.IsMortage == false)
          savings += field.MortageValue;
      }
      return savings;
    }

    public void RemovePlayer(Player player)
    {
      _players.Remove(player);
      _playerPositions.Remove(player);
      _diceThrows.Remove(player);
      _playerQueue = RemoveFromQueue(_playerQueue,player);
      RemoveOwnerInFieldsOf(player);
      StartAuction(player.OwnerShip.ToList());
      CurrentPlayer.Remove();
    }

    private void RemoveOwnerInFieldsOf(Player player)
    {
      foreach(IRentableField field in player.OwnerShip)
      {
        field.SetOwner(null);
      }
    }

    private static Queue<Player> RemoveFromQueue(Queue<Player> playerQueue, Player player)
    {
      Queue<Player> newPlayerQueue = new Queue<Player>();
      List<Player> players = playerQueue.ToList();
      players.Remove(player);
      foreach (var currentPlayer in players)
      {
        newPlayerQueue.Enqueue(currentPlayer);
      }
      return newPlayerQueue;
    }

    private List<IRentableField> _auction = new List<IRentableField>();
    public IReadOnlyList<IRentableField> AuctionFields
    {
      get { return _auction; }
    }

    public void StartAuction(List<IRentableField> rentableFields)
    {
      _auction = rentableFields;
    }

    public void FinishAuction()
    {
      _auction.Clear();
    }

    public void AuctionField(IRentableField rentableField, Dictionary<Player, int> bid)
    {
      if (SameBid(bid))
        throw new ArgumentException("The Players have the same bid value");
      if (_auction.Contains(rentableField) == false)
        throw new ArgumentException("This field is not available for the auction");
      rentableField.BuyInAuction(GetHighestbidPlayer(bid), GetHighestBidValue(bid));
    }

    private bool SameBid(Dictionary<Player, int> bid)
    {
      foreach(KeyValuePair<Player, int> currentPlayer in bid)
      {
        foreach(KeyValuePair<Player, int> allPlayer in bid)
        {
          if (currentPlayer.Key == allPlayer.Key)
            continue;
          if (currentPlayer.Value == allPlayer.Value)
            return true;
        }
      }
      return false;
    }

    private Player GetHighestbidPlayer(Dictionary<Player, int> bids)
    {
      int highestValue = 0;
      Player highestbidPlayer = null;
      foreach (KeyValuePair<Player, int> player in bids)
      {
        if (player.Value > highestValue)
        {
          highestValue = player.Value;
          highestbidPlayer = player.Key;
        }
      }
      return highestbidPlayer;
    }

    private int GetHighestBidValue(Dictionary<Player, int> bids)
    {
      int highestValue = 0;
      Player highestbidPlayer = null;
      foreach (KeyValuePair<Player, int> player in bids)
      {
        if (player.Value > highestValue)
        {
          highestValue = player.Value;
          highestbidPlayer = player.Key;
        }
      }
      return highestValue;
    }

    public bool IsGameOver()
    {
      if (Players.Count() == 1)
        return true;
      else
        return false;
    }

    public void MoveTo(Player player, int destination)
    {
      if (destination > PlayerPos[player])
      {
        SetPlayerPosition(player, destination);
      }
      else if (destination < PlayerPos[player])
      {
        SetPlayerPosition(player, destination);
        player.GetMoney(200);
      }
    }

    public void Move(Player player, int stepsToGo)
    {
      if(stepsToGo < 0) // Go Back
      {
        if(PlayerPos[player] + stepsToGo < 0)
        {
          SetPlayerPosition(player, (PlayerPos[player] + stepsToGo) + Fields.Count());
        }
        else // PlayerPos[player] + stepsToGo >= 0
        {
          SetPlayerPosition(player, (PlayerPos[player] + stepsToGo));
        }
      }
      else // stepstoGo >= 0 // GoForward
      {
        if(PlayerPos[player] + stepsToGo > Fields.Count() - 1) // Go over start Field
        {
          SetPlayerPos(player, PlayerPos[player] + stepsToGo - Fields.Count());
          player.GetMoney(200);
        }
        else // PlayerPos[player] + stepsToGo <= Fields.Count() - 1
        {
          SetPlayerPosition(player, (PlayerPos[player] + stepsToGo));
        }
      }
    }

    public static ICard GetCard(ICard[] cards)
    {
      Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
      return cards[random.Next(0, cards.Length - 1)];
    }
  }
}

