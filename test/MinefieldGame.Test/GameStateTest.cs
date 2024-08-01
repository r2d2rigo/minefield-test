using MinefieldGame.Gameplay;

namespace MinefieldGame.Test;

public class GameStateTest
{
    [Test]
    public void Constructor_InvalidBoardSize_ThrowsException()
    {
        Assert.Catch(typeof(ArgumentOutOfRangeException), () => new GameState(0, 3));
    }

    [Test]
    public void Constructor_InvalidStartingLives_ThrowsException()
    {
        Assert.Catch(typeof(ArgumentOutOfRangeException), () => new GameState(8, 0));
    }

    [TestCase(MovementDirection.Up)]
    [TestCase(MovementDirection.Down)]
    [TestCase(MovementDirection.Left)]
    [TestCase(MovementDirection.Right)]
    public void MovePlayer_ValidMovementDirection_MovesPlayer(MovementDirection direction)
    {
        var sut = GetSut();
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);

        sut.MovePlayer(direction);

        Assert.NotZero(sut.Score);
        Assert.NotZero(sut.PlayerPosition.X);
        Assert.NotZero(sut.PlayerPosition.Y);
        Assert.That(sut.LastMovementResult, Is.Not.EqualTo(MovementResult.CannotMove));
    }

    [Test]
    public void MovePlayer_StepsOnLandmine_DecrementsLives()
    {
        var sut = GetSut();

        sut.MovePlayer(MovementDirection.Right);

        Assert.NotZero(sut.Score);
        Assert.That(sut.Lives, Is.EqualTo(2));
    }

    [Test]
    public void MovePlayer_LosesAllLives_EndsGame()
    {
        var sut = GetSut();

        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);

        Assert.NotZero(sut.Score);
        Assert.That(sut.Lives, Is.EqualTo(0));
        Assert.IsTrue(sut.IsGameOver);
    }

    [Test]
    public void MovePlayer_ReachesBoardEnd_EndsGame()
    {
        var sut = GetSut();

        sut.MovePlayer(MovementDirection.Up);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Up);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Up);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Right);

        Assert.NotZero(sut.Score);
        Assert.That(sut.Lives, Is.EqualTo(1));
        Assert.IsTrue(sut.IsGameOver);
    }

    [Test]
    public void MovePlayer_StepsOnLandmine_RemovesLandmineFromBoard()
    {
        var sut = GetSut();

        sut.MovePlayer(MovementDirection.Right);
        sut.MovePlayer(MovementDirection.Left);
        sut.MovePlayer(MovementDirection.Right);

        Assert.NotZero(sut.Score);
        Assert.That(sut.Lives, Is.EqualTo(2));
        Assert.That(sut.LastMovementResult, Is.EqualTo(MovementResult.Moved));
    }

    [TestCase(MovementDirection.Left)]
    [TestCase(MovementDirection.Invalid)]
    public void MovePlayer_InvalidMovementDirection_DoesNotMovePlayer(MovementDirection direction)
    {
        var sut = GetSut();

        sut.MovePlayer(direction);

        Assert.Zero(sut.Score);
        Assert.That(sut.LastMovementResult, Is.EqualTo(MovementResult.CannotMove));
    }

    private static GameState GetSut()
    {
        return new GameState(8, 3);
    }
}
