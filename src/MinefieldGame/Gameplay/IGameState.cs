using System.Drawing;

namespace MinefieldGame.Gameplay;

public interface IGameState
{
    public Size BoardDimensions { get; }

    public Point PlayerPosition { get; }

    public int Score { get; }

    public int Lives { get; }

    public bool IsGameOver { get; }

    public MovementResult? LastMovementResult { get; }

    public void MovePlayer(MovementDirection direction);
}
