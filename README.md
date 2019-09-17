# Monopoly in WPF
This is a monopoly game developed using C#/.Net and WPF. 

### Screenshots
![game_beginn](https://user-images.githubusercontent.com/36839962/62157823-534d4e00-b30e-11e9-880a-c8826c981a22.PNG)

### Features
- Cubes Throw
- Buy a Property
- Level Up a Field from one house to a big hotel
- Take a Mortage and a property you own
- Sell a House you own
- Exchange a Field between two players
- Get and Leave the Prison
- Special Fields like Change and Community Chest

### Built with
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) and the [.Net Framework](https://dotnet.microsoft.com/)
- [NUnit](https://nunit.org/) for testing
- [GitHub Desktop](https://desktop.github.com/) to simplify github access
- [Inkscape](https://inkscape.org)
- and of course [Visual Studio](https://visualstudio.microsoft.com/)

### Code Examples
```
// Create a Game
Game game = new Game(new Player[] { new Player("XXX"), new Player("YYY") });

// Move forward
game.GoForward(game.CurrentPlayer);

// Get the next Player
game.NextPlayer();

// Get the last payment made to display it
game.LastPayMent[game.CurrentPlayer];

// Upgrade a Field
StreetField streetToUpdate = (StreetField)game.CurrentPlayer.OwnerShip[streetNum];
streetToUpdate.LevelUp(game.CurrentPlayer, level);
```

### License
This project is licensed under the MIT License - see the LICENSE.md file for details
