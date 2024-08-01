using MinefieldGame.Input;
using MinefieldGame.UI;

namespace MinefieldGame.Gameplay;

public class GameRunner
    : IGameRunner
{
    private static readonly int BOARD_SIZE = 8;
    private static readonly int PLAYER_LIVES = 3;

    private readonly IInputParser _inputParser;
    private readonly IInterfaceFormatter _interfaceFormatter;

    private IGameState _currentGameState;

    public GameRunner(
        IInputParser inputParser,
        IInterfaceFormatter interfaceFormatter)
    {
        _inputParser = inputParser;
        _interfaceFormatter = interfaceFormatter;

        _currentGameState = new GameState(BOARD_SIZE, PLAYER_LIVES);
    }

    public void RunGame()
    {
        while (!_currentGameState.IsGameOver)
        {
            Console.Clear();

            Console.WriteLine(_interfaceFormatter.FormatGameState(_currentGameState));
            Console.Write("Enter your move: ");

            var input = Console.ReadLine();
            var direction = _inputParser.ParseInput(input);

            _currentGameState.MovePlayer(direction);
        }

        Console.Clear();

        Console.WriteLine(_interfaceFormatter.FormatGameState(_currentGameState));
    }
}
