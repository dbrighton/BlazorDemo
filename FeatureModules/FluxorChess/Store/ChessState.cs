using FluxorChess.Models;
using FluxorChess.Utils;

namespace FluxorChess.Store;

/// <summary>
///     Represents the state of the chess game.
/// </summary>
/// <remarks>
///     This class is used to store the current state of the chess game.
/// </remarks>
public record ChessState(List<ChessGame> Games, ChessGame CurrentGame, bool HubConnected);

public class ChessFeature : Feature<ChessState>
{
    public override string GetName()
    {
        return nameof(ChessFeature);
    }

    /// <summary>
    ///     Gets the initial state of the chess game.
    /// </summary>
    /// <returns>The initial state of the chess game.</returns>
    /// <remarks>
    ///     This method is used to initialize the state of the chess game.
    /// </remarks>
    protected override ChessState GetInitialState()
    {
      
        return new ChessState(new List<ChessGame>(), new ChessGame(),false);
    }
}