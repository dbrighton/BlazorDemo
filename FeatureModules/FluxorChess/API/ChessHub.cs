

// Ignore Spelling: Fluxor API

namespace FluxorChess.API;

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
        _ea.GetEvent<StartNewGamePrismEvent>().Publish(player);
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
        // This line of code is using the GetEvent method from the EventAggregator to retrieve the JoinGamePrismEvent
        // It then calls the Publish method on the event, passing in the gameInfo object as the parameter.
        // This will be handled by the ChessGameService
        _ea.GetEvent<JoinGamePrismEvent>().Publish(new JoinGameRequest(gameInfo.GameInfo,gameInfo.Player, Clients.Caller));
        return Task.CompletedTask;
    }

    [HubMethodName(HubConstants.MoveChessPiece)]
    public Task MoveChessPiece(ChessPiece chessPiece,string targetCellId)
    {
        var moveRequest =new  MoveChessPieceRequest(chessPiece, targetCellId, Clients.Caller);
        
        _ea.GetEvent<MoveChessPiecePrismEvent>().Publish(moveRequest);
        return Task.CompletedTask;
    }
}