namespace UserFeatureModule.Store;

public class UserEffects
{
    private readonly HubConnection _hubConnection;
    private readonly ILogger<UserEffects> _log;

    public UserEffects(ILogger<UserEffects> logger, NavigationManager navigationManager)
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
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(true));

            return;
        }

        await _hubConnection.StartAsync();

        _hubConnection.Reconnecting += ex =>
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += connectionId =>
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(true));
            return Task.CompletedTask;
        };

        _hubConnection.On<Person>("PersonLogin", Person =>
        {
            // TODO: Dispatch action to update state
        });

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new AuthHubSetConnectedAction(true));
            //dispatcher.Dispatch(new GenericSuccessAction("Auth Hub Connected"));
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}", _hubConnection.State);
            dispatcher.Dispatch(new AuthHubSetConnectedAction(false));
            dispatcher.Dispatch(new GenericErrorAction("Auth Hub Not Connected!"));
        }
    }
}