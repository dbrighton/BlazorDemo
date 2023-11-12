
namespace ChessFeatureModule.API;

public class ChessGameService
{
    private static readonly List<ChessGame> _chessGames = new();
    private readonly IEventAggregator _ea;
    private readonly IHubContext<ChessHub> _hub;
    private readonly ILogger<ChessGameService> _Log;

    public ChessGameService(ILogger<ChessGameService> log, IEventAggregator ea, IHubContext<ChessHub> hub)
    {
        _Log = log;
        _ea = ea;
        _hub = hub;

        _ea.GetEvent<StartNewGamePrismEvent>().Subscribe(player =>
        {
            var game = new ChessGame
            {
                GameInfo = new GameInfo
                {
                    GameId = Guid.NewGuid(),
                    CreateBy = player,
                    GameStatus = GameStatus.WaitingForPlayers
                }
            };
            game.PlayerOne = player;
            _chessGames.Add(game);

            _hub.Clients.All.SendAsync(HubConstants.GameListChanged, _chessGames);
        });
        _ea.GetEvent<JoinGamePrismEvent>().Subscribe(joinGameRequest =>
        {
            try
            {
                var game = _chessGames.First(i => i.GameInfo?.GameId == joinGameRequest.GameInfo.GameId);


                // If player one is null then set player one  this will be the first player
                // to join the game
                if (game.PlayerOne != null && game.PlayerTwo == null && game.GameInfo != null)
                {
                    game.GameInfo.GameStatus = GameStatus.InProgress;
                    game.GameInfo.LastUpdateTimeStamp = DateTime.UtcNow;


                    joinGameRequest.HubCaller?.SendAsync(HubConstants.PlayerOneJoinGame, game);
                }
                // If player two is null then set player one  this will be the first second
                // to join the game
                else if (game.PlayerTwo == null)
                {
                    game.PlayerTwo = joinGameRequest.Player;
                    game.GameInfo.GameStatus = GameStatus.InProgress;
                }
                else
                {
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.GenericInfo, "Joining as Observer");
                }

                if (game != null)
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.ChessGameSateChanged, game);
                else
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.GenericError, "Game does not exists");
            }
            catch (Exception e)
            {
                _Log.LogError(e.Message);
            }
        });
        _ea.GetEvent<MoveChessPiecePrismEvent>().Subscribe(game =>
        {
            var target = _chessGames.FirstOrDefault(i => i.GameInfo.GameId == game.GameInfo.GameId);
            _chessGames.Remove(target);
            _chessGames.Add(game);

            //toggle the current player
            game.CurrentPlayer.Color = game.CurrentPlayer.Color == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;

            _hub.Clients.All.SendAsync(HubConstants.ChessGameSateChanged, game);

            _Log.LogInformation("ChessGameSateChanged");
            _Log.LogInformation("");
            _Log.LogInformation(game.PrintGame());
        });
        _ea.GetEvent<RefreshGameListPrismEvent>().Subscribe(() =>
        {
            _hub.Clients.All.SendAsync(HubConstants.GameListChanged, _chessGames);
        });
        _ea.GetEvent<ResignGamePrismEvent>().Subscribe(game =>
        {
            var target = _chessGames.FirstOrDefault(i => i.GameInfo.GameId == game.GameInfo.GameId);
            _chessGames.Remove(target);
           _hub.Clients.All.SendAsync(HubConstants.GameListChanged, _chessGames);
           _hub.Clients.All.SendAsync(HubConstants.ResignGame, target);
        });
    }
}