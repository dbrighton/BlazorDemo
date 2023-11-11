namespace ChessFeatureModule.API;

/// <summary>
/// Represents a hub for managing chess games.
/// </summary>
public class ChessHub : Hub
{
    private readonly ILogger<ChessHub> _log;
    private readonly IEventAggregator _ea;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChessHub"/> class.
    /// </summary>
    /// <param name="log">The logger.</param>
    /// <param name="ea">The event aggregator.</param>
    public ChessHub(ILogger<ChessHub> log, IEventAggregator ea)
    {
        _log = log;
        _ea = ea;
    }

    /// <summary>
    /// Starts a new game with the specified player.
    /// </summary>
    /// <param name="player">The player to start the game with.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HubMethodName(HubConstants.StartNewGame)]
    public Task StartNewGame(ChessPlayer player)
    {
        // Publishes the StartNewGamePrismEvent with the player as the parameter
        // witch will be handled by the ChessGameService
        // player.HubClientCaller = Clients.Caller;
        _ea.GetEvent<StartNewGamePrismEvent>().Publish(player);
        return Task.CompletedTask;
    }

    [HubMethodName(HubConstants.ResignGame)]
    public Task ResignGame(ChessGame game)
    {
        _ea.GetEvent<ResignGamePrismEvent>().Publish(game);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method for joining a game.
    /// </summary>
    /// <param name="gameInfo">The game information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [HubMethodName(HubConstants.JoinGame)]
    public Task JoinGame(JoinGameRequest gameInfo)
    {
        // Retrieve the JoinGamePrismEvent from the EventAggregator using the GetEvent method
        // Publish the event by calling the Publish method and passing in the gameInfo object as the parameter
        // The ChessGameService will handle this event
        _ea.GetEvent<JoinGamePrismEvent>().Publish(new JoinGameRequest(gameInfo.GameInfo, gameInfo.Player, Clients.Caller));
        return Task.CompletedTask;
    }


    // This method is called when the client requests the game list
    [HubMethodName(HubConstants.GetGameList)]
    public Task GetGameList()
    {
        // Publish an event to refresh the game list
        _ea.GetEvent<RefreshGameListPrismEvent>().Publish();

        // Return a completed task
        return Task.CompletedTask;
    }

    // This method is called when the state of a chess game has changed
    [HubMethodName(HubConstants.ChessGameSateChanged)]
    public Task ChessGameSateChanged(ChessGame game)
    {
        // Publish an event using the Prism event aggregator to notify subscribers that a chess piece has been moved
        _ea.GetEvent<MoveChessPiecePrismEvent>().Publish(game);

        // Return a completed task
        return Task.CompletedTask;
    }
}