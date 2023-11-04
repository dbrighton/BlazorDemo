using FluxorChess.Utils;

namespace FluxorChess.API;
public class ChessHub : Hub
{
  
    private readonly ILogger<ChessHub> _log;
    private readonly IEventAggregator _ea;
  

    public ChessHub(ILogger<ChessHub> log, IEventAggregator ea )
    {
        _log = log;
        _ea= ea;
    }

    [HubMethodName(HubConstants.StartNewGame)]
    public  Task StartNewGame(ChessPlayer player)
    {
        var game = new ChessGame
        {
            CreateBy = player.Name,
            PlayerOne = player
        };
        game.ResetGame();
        _ea.GetEvent<CreateGamePrismEvent>().Publish(game);
        
        return Task.CompletedTask;
    }

    [HubMethodName(HubConstants.JoinGame)]
    public Task JoinGame(ChessGame game)
    {
        game.HubClients = Clients;
        _ea.GetEvent<JoinGamePrismEvent>().Publish(game );
        return Task.CompletedTask;
    }
}