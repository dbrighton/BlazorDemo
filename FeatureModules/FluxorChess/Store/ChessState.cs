namespace FluxorChess.Store;

/// <summary>
///     Represents the state of the chess game.
/// </summary>
/// <remarks>
///     This class is used to store the current state of the chess game.
/// </remarks>
public class ChessState
{
    public List<GameInfo> Games { get; set; }=new();
    public Models.ChessGame? CurrentGame { get; set; }
    public bool HubConnected { get; set; }
}

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
    protected override ChessState GetInitialState() {
       

        return new ChessState
        {
            Games = new List<GameInfo>()
        };
    }
}