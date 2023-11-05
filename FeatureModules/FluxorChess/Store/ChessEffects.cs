namespace FluxorChess.Store;

public class ChessEffects
{
    private readonly IHubContext<ChessHub> _hub;
    private readonly HubConnection _hubConnection;
    private readonly ILogger<ChessEffects> _log;


    public ChessEffects(ILogger<ChessEffects> log, NavigationManager navigationManager, IHubContext<ChessHub> hub)
    {
        _log = log;
        _hub = hub;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chessHub"))
            .WithAutomaticReconnect()
            .Build();
    }

    [EffectMethod(typeof(StartHubEffectsAction))]
    public async Task Start(IDispatcher dispatcher)
    {
        if (_hubConnection.State == HubConnectionState.Connected) dispatcher.Dispatch(new HubSetConnectedReducerAction(true));

        _hubConnection.Reconnecting += ex =>
        {
            dispatcher.Dispatch(new HubSetConnectedReducerAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += connectionId =>
        {
            dispatcher.Dispatch(new HubSetConnectedReducerAction(true));
            return Task.CompletedTask;
        };

        _hubConnection.On<ChessGame>(HubConstants.ChessGameSateChanged, payload =>
        {
            dispatcher.Dispatch(new GameUpdatedReducerAction(payload));
        });

        _hubConnection.On<List<GameInfo>>(HubConstants.GameListChanged, payload =>
        {
            dispatcher.Dispatch(new GameListChangedReducerAction(payload));
            dispatcher.Dispatch(new GenericSuccessAction("Chess Game List Updated"));
        });

        _hubConnection.On<string>(HubConstants.GenericError, payload =>
        {
            dispatcher.Dispatch(new GenericErrorAction(payload));
        });
        

        
        await _hubConnection.StartAsync();

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new HubSetConnectedReducerAction(true));
            dispatcher.Dispatch(new GenericSuccessAction("Chess Hub Connected"));
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}", _hubConnection.State);
            dispatcher.Dispatch(new HubSetConnectedReducerAction(false));
            dispatcher.Dispatch(new GenericErrorAction("Chess Hub Not Connected!"));
        }
    }

    [EffectMethod]
    public async Task CreateGame(NewGameEffectsAction action, IDispatcher dispatcher)
    {
        try
        {
            await _hubConnection.SendAsync(HubConstants.StartNewGame, action.Player);
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GenericErrorAction(ex.Message));
        }
    }

    [EffectMethod]
    public async Task OnJoinGameEffectsAction(JoinGameEffectsAction action, IDispatcher dispatcher)
    {
        try
        {
            await _hubConnection.SendAsync(HubConstants.JoinGame, action.Game);
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GenericErrorAction(ex.Message));
        }
    }

    [EffectMethod]
    public async Task OnMoveChessPieceEffectsAction(MoveChessPieceEffectsAction action, IDispatcher dispatcher)
    {
        try
        {
            await _hubConnection.SendAsync(HubConstants.MoveChessPiece, action.ChessPiece, action.TargetCellId);
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GenericErrorAction(ex.Message));
        }
    }
}

