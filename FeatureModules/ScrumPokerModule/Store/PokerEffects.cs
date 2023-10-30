namespace ScrumPokerFeatureModule.Store;

public class PokerEffects
{
    private readonly HubConnection _hubConnection;
    private readonly ILogger<PokerEffects> _log;

    public PokerEffects(ILogger<PokerEffects> logger, NavigationManager navigationManager)
    {
        _log = logger;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/authHub"))
            .WithAutomaticReconnect()
            .Build();
    }

    [EffectMethod(typeof(PokerHubStartAction))]
    public async Task Start(IDispatcher dispatcher)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(true));
            return;
        }

        await _hubConnection.StartAsync();

        _hubConnection.Reconnecting += ex =>
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += connectionId =>
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(true));
            return Task.CompletedTask;
        };

        _hubConnection.On<Person>("PersonLogin", Person =>
        {
            // TODO: Dispatch action to update state
        });

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(true));
            dispatcher.Dispatch(new GenericSuccessAction("Poker Hub Connected"));
            dispatcher.Dispatch(new GetPokerSessionsAction());
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}", _hubConnection.State);
            dispatcher.Dispatch(new PokerHubSetConnectedAction(false));
            dispatcher.Dispatch(new GenericErrorAction("Poker Hub Not Connected!"));
        }
    }

    [EffectMethod(typeof(GetPokerSessionsAction))]
    public async Task GetPokerSessions(IDispatcher dispatcher)
    {
        try
        {
            var sessions = await _hubConnection.InvokeAsync<List<ScrumPokerGame>>(Constants.GetPokerSessions);
            dispatcher.Dispatch(new PokerSessionsChangedSuccessAction(sessions));
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error getting poker sessions");
            dispatcher.Dispatch(new GenericErrorAction("Error getting poker sessions"));
        }
    }
}