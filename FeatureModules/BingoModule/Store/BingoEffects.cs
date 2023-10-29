namespace BingoFeatureModule.Store;

public class BingoEffects
{
    private readonly ILogger<BingoEffects> _log;
    private readonly HubConnection _hubConnection;

    public BingoEffects(ILogger<BingoEffects>logger, NavigationManager navigationManager)
    {
        _log = logger;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/bingoHub"))
            .WithAutomaticReconnect()
            .Build();
    }

    [EffectMethod(typeof(BingoHubStartAction))]
    public async Task Start(IDispatcher dispatcher)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new BingoHubSetConnectedAction(true));
            return;
        }

        await _hubConnection.StartAsync();

        _hubConnection.Reconnecting += (ex) =>
        {
            dispatcher.Dispatch(new BingoHubSetConnectedAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += (connectionId) =>
        {
            dispatcher.Dispatch(new BingoHubSetConnectedAction(true));
            return Task.CompletedTask;
        };

        
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new BingoHubSetConnectedAction(true));
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}",_hubConnection.State);
            dispatcher.Dispatch(new BingoHubSetConnectedAction(false));
        }
        
    }
}