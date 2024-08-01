using System.Drawing;

namespace MinefieldGame.Gameplay;

public class GameState
    : IGameState
{
    private static readonly int BOARD_SIZE = 8;

    private bool[,] _minefield;

    public GameState(
        int boardSize,
        int startingLives)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan<int>(boardSize, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan<int>(startingLives, 1);

        _minefield = new bool[boardSize, boardSize];

        // Fixed board pattern
        for (var i = 0; i < boardSize * boardSize; i++)
        {
            var row = i / boardSize;
            var column = i % boardSize;

            _minefield[row, column] = i % 3 == 0;
        }

        BoardDimensions = new Size(boardSize, boardSize);
        PlayerPosition = new Point(0, boardSize / 2);
        Score = 0;
        Lives = startingLives;
    }

    public Size BoardDimensions { get; private set; }

    public Point PlayerPosition { get; private set; }

    public int Score { get; private set; }

    public int Lives { get; private set; }

    public MovementResult? LastMovementResult { get; private set; }

    public bool IsGameOver => Lives == 0 || PlayerPosition.X >= BoardDimensions.Width;

    public void MovePlayer(MovementDirection direction)
    {
        var newPosition = direction switch
        {
            MovementDirection.Up => new Point(PlayerPosition.X, PlayerPosition.Y + 1),
            MovementDirection.Down => new Point(PlayerPosition.X, PlayerPosition.Y - 1),
            MovementDirection.Left => new Point(PlayerPosition.X - 1, PlayerPosition.Y),
            MovementDirection.Right => new Point(PlayerPosition.X + 1, PlayerPosition.Y),
            _ => new Point(int.MinValue, int.MinValue),
        };

        if (newPosition.X < 0 ||
            newPosition.Y < 0 ||
            newPosition.Y >= BoardDimensions.Height)
        {
            LastMovementResult = MovementResult.CannotMove;

            return;
        }

        PlayerPosition = newPosition;
        Score++;

        if (PlayerPosition.X < BoardDimensions.Width &&
            _minefield[PlayerPosition.X, PlayerPosition.Y] == true)
        {
            _minefield[PlayerPosition.X, PlayerPosition.Y] = false;
            Lives--;

            LastMovementResult = MovementResult.SteppedOnMine;

            return;
        }

        LastMovementResult = MovementResult.Moved;
    }
}
