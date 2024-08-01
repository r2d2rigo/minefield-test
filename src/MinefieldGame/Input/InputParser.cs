using MinefieldGame.Gameplay;

namespace MinefieldGame.Input;

public class InputParser
    : IInputParser
{
    private const string UP_COMMAND = "up";
    private const string DOWN_COMMAND = "down";
    private const string LEFT_COMMAND = "left";
    private const string RIGHT_COMMAND = "right";

    public MovementDirection ParseInput(string? input)
    {
        var trimmendInput = input?.Trim()?.ToLowerInvariant();

        return trimmendInput switch
        {
            UP_COMMAND => MovementDirection.Up,
            DOWN_COMMAND => MovementDirection.Down,
            LEFT_COMMAND => MovementDirection.Left,
            RIGHT_COMMAND => MovementDirection.Right,
            _ => MovementDirection.Invalid,
        };
    }
}
