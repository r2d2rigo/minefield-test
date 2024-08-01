using MinefieldGame.Gameplay;
using MinefieldGame.Input;
using MinefieldGame.UI;

var runner = new GameRunner(
    new InputParser(),
    new InterfaceFormatter());

runner.RunGame();

Console.ReadKey();