using MinefieldGame.Gameplay;
using MinefieldGame.Input;

namespace MinefieldGame.Test;

public class InputParserTest
{
    [TestCase("north")]
    [TestCase("downright")]
    [TestCase("")]
    [TestCase("     ")]
    [TestCase(null)]

    public void ParseInput_InvalidInput_ReturnsFalseAndInvalidDirection(string input)
    {
        var parser = GetSut();

        var direction = parser.ParseInput(input);

        Assert.That(direction, Is.EqualTo(MovementDirection.Invalid));
    }

    [TestCase("up", MovementDirection.Up)]
    [TestCase("down", MovementDirection.Down)]
    [TestCase("left", MovementDirection.Left)]
    [TestCase("right", MovementDirection.Right)]
    [TestCase("UP", MovementDirection.Up)]
    [TestCase("DOWN", MovementDirection.Down)]
    [TestCase("LEFT", MovementDirection.Left)]
    [TestCase("RIGHT", MovementDirection.Right)]
    [TestCase("  up", MovementDirection.Up)]
    [TestCase("up  ", MovementDirection.Up)]
    public void ParseInput_ValidInput_ReturnsTrueAndCorrectDirection(string input, MovementDirection expectedDirection)
    {
        var parser = GetSut();

        var direction = parser.ParseInput(input);

        Assert.That(direction, Is.EqualTo(expectedDirection));
    }

    private static InputParser GetSut()
    {
        return new InputParser();
    }
}
