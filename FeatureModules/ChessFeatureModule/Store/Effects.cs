namespace ChessFeatureModule.Store;

public class Effects
{
    private readonly IHubContext<ChessHub> _hub;
    private readonly ILogger<Effects> _log;
    private readonly HubConnection? _hubConnection;


    public Effects(ILogger<Effects> log, NavigationManager navigationManager, IHubContext<ChessHub> hub)
    {
        _log = log;
        _hub = hub;

        if (_hubConnection == null)
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chessHub"))
                .WithAutomaticReconnect()
                .Build();
    }

    [EffectMethod(typeof(StartHubEffectsAction))]
    public async Task Start(IDispatcher dispatcher)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new HubSetConnectedReducerAction(true));
            return;
        }

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

        _hubConnection.On<ChessGame>(HubConstants.PlayerOneJoinGame, game =>
            {
                dispatcher.Dispatch(new JoinGameReducerAction(game));
                dispatcher.Dispatch(new JoinGameAction(game));
            });

        _hubConnection.On<ChessGame>(HubConstants.ChessGameSateChanged, chessGame =>
            {
                dispatcher.Dispatch(new GameUpdatedReducerAction(chessGame));
                dispatcher.Dispatch(new GameUpdateAction(chessGame));
            });

        _hubConnection.On<List<ChessGame>>(HubConstants.GameListChanged, chessGameList =>
        {
            List<GameInfo> gameInfos = chessGameList.Select(i => i.GameInfo).ToList()!;

            if (!gameInfos.Any()) return;

            dispatcher.Dispatch(new GameListChangedReducerAction(gameInfos));
            dispatcher.Dispatch(new GenericSuccessAction("Chess Game List Updated"));
        });

        _hubConnection.On<string>(HubConstants.GenericError, payload =>
            {
                dispatcher.Dispatch(new GenericErrorAction(payload));
            });

        _hubConnection.On<ChessGame>(HubConstants.ResignGame, game =>
        {
            dispatcher.Dispatch(new GameDeletedReducerAction(game));
            dispatcher.Dispatch(new NavigateAction("ChessHome"));
        });

        await _hubConnection.StartAsync();

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new HubSetConnectedReducerAction(true));
            // dispatcher.Dispatch(new GenericSuccessAction("Chess Hub Connected"));
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
            await _hubConnection!.SendAsync(HubConstants.StartNewGame, action.Player);
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
            await _hubConnection.SendAsync(HubConstants.JoinGame, action.JoinGameRequest);
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GenericErrorAction(ex.Message));
        }
    }
    
    [EffectMethod(typeof(RefreshGameListEffectsAction))]
    public async Task OnRefreshGameListEffectsAction(IDispatcher dispatcher)
    {
        try
        {
            await _hubConnection.SendAsync(HubConstants.GetGameList);
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
            await _hubConnection.SendAsync(HubConstants.ChessGameSateChanged, action.Game);
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GenericErrorAction(ex.Message));
        }
    }

    [EffectMethod]
    public async Task OnResignGameEffectsAction(ResignGameEffectsAction action, IDispatcher dispatcher)
    {
        try
        {
            await _hubConnection.SendAsync(HubConstants.ResignGame, action.Game);
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new GenericErrorAction(ex.Message));
        }
    }
}