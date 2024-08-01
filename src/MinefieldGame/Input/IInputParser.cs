using MinefieldGame.Gameplay;

namespace MinefieldGame.Input;

public interface IInputParser
{
    public MovementDirection ParseInput(string? input);
}
