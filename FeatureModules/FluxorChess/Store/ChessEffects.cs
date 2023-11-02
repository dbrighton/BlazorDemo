using FluxorChess.Models;

namespace FluxorChess.Store;


public class ChessEffects
{
    private readonly ILogger<ChessEffects> _log;
    private readonly IHubContext<ChessHub> _hub;
    private readonly HubConnection _hubConnection;


    public ChessEffects(ILogger<ChessEffects> log, NavigationManager navigationManager, IHubContext<ChessHub> hub)
    {
        _log = log;
        _hub = hub;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chessHub"))
            .WithAutomaticReconnect()
            .Build();
    }

    [EffectMethod(typeof(StartHubAction))]
    public async  Task Start(IDispatcher dispatcher){

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new HubSetConnectedAction(true));
            
        }

        _hubConnection.Reconnecting += ex =>
        {
            dispatcher.Dispatch(new HubSetConnectedAction(false));
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += connectionId =>
        {
            dispatcher.Dispatch(new HubSetConnectedAction(true));
            return Task.CompletedTask;
        };

        // _hubConnection.On<List<ChessPiece>>(SignalRConstants.ChessGameSateChanged, payload =>
        // {
        //     dispatcher.Dispatch(new ChessSetStateAction(payload));
        //     dispatcher.Dispatch(new GenericSuccessAction("Chess Game Updated"));
        // });

        await _hubConnection.StartAsync();

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            dispatcher.Dispatch(new HubSetConnectedAction(true));
            dispatcher.Dispatch(new GenericSuccessAction("Bingo Hub Connected"));
        }
        else
        {
            _log.LogCritical("HubConnectionState:{State}", _hubConnection.State);
            dispatcher.Dispatch(new HubSetConnectedAction(false));
            dispatcher.Dispatch(new GenericErrorAction("Bingo Hub Not Connected!"));
        }

        return;
    }

}
