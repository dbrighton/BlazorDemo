namespace ScrumPokerFeatureModule.Store;

public class PokerEffects
{
    private readonly ILogger<PokerEffects> _log;
    private readonly HubConnection _hubConnection;

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
        await _hubConnection.StartAsync();

        _hubConnection.Reconnecting += (ex) =>
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += (connectionId) =>
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(true));
            return Task.CompletedTask;
        };

        _hubConnection.On<Person>("PersonLogin", (Person) =>
        {
            // TODO: Dispatch action to update state
        });

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new PokerHubSetConnectedAction(true));
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}", _hubConnection.State);
            dispatcher.Dispatch(new PokerHubSetConnectedAction(false));
        }
    }
}