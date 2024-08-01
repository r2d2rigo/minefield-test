using MinefieldGame.Gameplay;
using MinefieldGame.UI;
using Moq;
using System.Drawing;

namespace MinefieldGame.Test;

public class InterfaceFormatterTest
{
    [TestCase(null)]
    public void FormatGameState_WithInvalidGameState_ReturnsEmptyString(IGameState gameState)
    {
        var formatter = GetSut();
        var actualOutput = formatter.FormatGameState(gameState);

        Assert.IsNotNull(actualOutput);
        Assert.IsEmpty(actualOutput);
    }

    [Test]
    public void FormatGameState_WithValidGameState_ReturnsFormattedString()
    {
        var expectedOutput =
            "Player position: A4" + Environment.NewLine +
            "Lives: 3" + Environment.NewLine +
            "Score: 0" + Environment.NewLine;

        var gameState = new Mock<IGameState>();
        gameState.Setup(x => x.PlayerPosition).Returns(new Point(0, 3));
        gameState.Setup(x => x.Lives).Returns(3);
        gameState.Setup(x => x.Score).Returns(0);

        var formatter = GetSut();
        var actualOutput = formatter.FormatGameState(gameState.Object);

        Assert.IsNotNull(actualOutput);
        Assert.IsNotEmpty(actualOutput);
        Assert.That(actualOutput, Is.EqualTo(expectedOutput));
    }

    [TestCase(MovementResult.CannotMove, "Cannot move in that direction.")]
    [TestCase(MovementResult.SteppedOnMine, "Oh no! You stepped on a landmine and lost a life!")]
    public void FormatGameState_WithLastMovementResult_ReturnsFormattedString(MovementResult lastMovement, string expectedMessage)
    {
        var expectedOutput =
            expectedMessage + Environment.NewLine +
            "Player position: A4" + Environment.NewLine +
            "Lives: 3" + Environment.NewLine +
            "Score: 0" + Environment.NewLine;

        var gameState = new Mock<IGameState>();
        gameState.Setup(x => x.PlayerPosition).Returns(new Point(0, 3));
        gameState.Setup(x => x.Lives).Returns(3);
        gameState.Setup(x => x.Score).Returns(0);
        gameState.Setup(x => x.LastMovementResult).Returns(lastMovement);

        var formatter = GetSut();
        var actualOutput = formatter.FormatGameState(gameState.Object);

        Assert.IsNotNull(actualOutput);
        Assert.IsNotEmpty(actualOutput);
        Assert.That(actualOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void FormatGameState_WithGameLost_ReturnsFormattedString()
    {
        var expectedOutput =
            "Oh no! You ran out of lives. Better luck next time!" + Environment.NewLine;

        var gameState = new Mock<IGameState>();
        gameState.Setup(x => x.PlayerPosition).Returns(new Point(0, 3));
        gameState.Setup(x => x.Lives).Returns(0);
        gameState.Setup(x => x.IsGameOver).Returns(true);

        var formatter = GetSut();
        var actualOutput = formatter.FormatGameState(gameState.Object);

        Assert.IsNotNull(actualOutput);
        Assert.IsNotEmpty(actualOutput);
        Assert.That(actualOutput, Is.EqualTo(expectedOutput));
    }

    [Test]
    public void FormatGameState_WithGameWon_ReturnsFormattedString()
    {
        var expectedOutput =
            $"Congratulations! You reached the other side of the board." + Environment.NewLine +
            $"Final score: 99" + Environment.NewLine;

        var gameState = new Mock<IGameState>();
        gameState.Setup(x => x.PlayerPosition).Returns(new Point(0, 3));
        gameState.Setup(x => x.Lives).Returns(3);
        gameState.Setup(x => x.IsGameOver).Returns(true);
        gameState.Setup(x => x.Score).Returns(99);

        var formatter = GetSut();
        var actualOutput = formatter.FormatGameState(gameState.Object);

        Assert.IsNotNull(actualOutput);
        Assert.IsNotEmpty(actualOutput);
        Assert.That(actualOutput, Is.EqualTo(expectedOutput));
    }

    private static InterfaceFormatter GetSut()
    {
        return new InterfaceFormatter();
    }
}