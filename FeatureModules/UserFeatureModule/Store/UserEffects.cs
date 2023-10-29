namespace UserFeatureModule.Store;

public class AuthHubEffects
{
    private readonly HubConnection _hubConnection;
    private readonly ILogger<AuthHubEffects> _log;

    public AuthHubEffects(ILogger<AuthHubEffects>logger, NavigationManager navigationManager)
    {
        _log = logger;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/authHub"))
            .WithAutomaticReconnect()
            .Build();
    }

    [EffectMethod(typeof(AuthHubStartAction))]
    public async Task Start(IDispatcher dispatcher)
    {
        await _hubConnection.StartAsync();

        _hubConnection.Reconnecting += (ex) =>
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += (connectionId) =>
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(true));
            return Task.CompletedTask;
        };

        _hubConnection.On<Person>("PersonLogin", (Person) =>
        {
            // TODO: Dispatch action to update state
        });

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(true));
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}",_hubConnection.State);
            dispatcher.Dispatch(new AuthHubSetConnectedAction(false));
        }
    }
}