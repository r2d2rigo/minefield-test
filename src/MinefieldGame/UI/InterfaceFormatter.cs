using System.Text;
using MinefieldGame.Gameplay;

namespace MinefieldGame.UI;

public class InterfaceFormatter
    : IInterfaceFormatter
{
    public string FormatGameState(IGameState gameState)
    {
        if (gameState == null)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();

        if (gameState.IsGameOver)
        {
            if (gameState.Lives <= 0)
            {
                FormatGameLost(gameState, builder);
            }
            else
            {
                FormatGameWon(gameState, builder);
            }
        }
        else
        {
            FormatGameInterface(gameState, builder);
        }

        return builder.ToString();
    }

    private static void FormatGameLost(IGameState gameState, StringBuilder builder)
    {
        builder.AppendLine($"Oh no! You ran out of lives. Better luck next time!");
    }

    private static void FormatGameWon(IGameState gameState, StringBuilder builder)
    {
        builder.AppendLine($"Congratulations! You reached the other side of the board.");
        builder.AppendLine($"Final score: {gameState.Score}");
    }

    private static void FormatGameInterface(IGameState gameState, StringBuilder builder)
    {
        switch (gameState.LastMovementResult)
        {
            case MovementResult.CannotMove:
                builder.AppendLine("Cannot move in that direction.");
                break;
            case MovementResult.SteppedOnMine:
                builder.AppendLine("Oh no! You stepped on a landmine and lost a life!");
                break;
        }

        builder.AppendLine($"Player position: {(char)('A' + gameState.PlayerPosition.X)}{gameState.PlayerPosition.Y + 1}");
        builder.AppendLine($"Lives: {gameState.Lives}");
        builder.AppendLine($"Score: {gameState.Score}");
    }
}
