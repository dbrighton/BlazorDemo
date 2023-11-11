namespace ChessFeatureModule.Store;

/// <summary>
/// Represents the state of the chess game.
/// </summary>
public class ChessState
{
    /// <summary>
    /// Gets or sets the list of games.
    /// </summary>
    public List<GameInfo> Games { get; set; } = new();

    /// <summary>
    /// Gets or sets the current game.
    /// </summary>
    public Models.ChessGame? CurrentGame { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the hub is connected.
    /// </summary>
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
    protected override ChessState GetInitialState()
    {
       

        return new ChessState
        {
            Games = new List<GameInfo>()
        };
    }
}