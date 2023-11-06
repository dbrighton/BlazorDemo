namespace FluxorChess.API;

public class ChessGameService
{
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
                    GameStatus = GameStatus.WaitingForPlayer
                }
            };
            ChessGames.Add(game);
            _hub.Clients.All.SendAsync(HubConstants.GameListChanged, ChessGames);
        });
        _ea.GetEvent<JoinGamePrismEvent>().Subscribe(joinGameRequest =>
        {
            try
            {
                var target = ChessGames.FirstOrDefault(i => i.GameInfo.GameId == joinGameRequest.GameInfo.GameId);


                // If player one is null then set player one  this will be the first player
                // to join the game
                if (target.PlayerOne == null)
                {
                    target.PlayerOne = joinGameRequest.Player;
                    target.GameInfo.GameStatus = GameStatus.InProgress;
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.JoinGame, target);
                }
                // If player two is null then set player one  this will be the first second
                // to join the game
                else if (target.PlayerTwo == null)
                {
                    target.PlayerTwo = joinGameRequest.Player;
                    target.GameInfo.GameStatus = GameStatus.InProgress;
                }
                else
                {
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.GenericInfo, "Joining as Observer");
                }
                
                if (target != null)
                {
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.ChessGameSateChanged, target);
                }
                else
                {
                    joinGameRequest.HubCaller?.SendAsync(HubConstants.GenericError, "Game does not exists");
                }
            }
            catch (Exception e)
            {
                _Log.LogError(e.Message);
            }
        });
        _ea.GetEvent<MoveChessPiecePrismEvent>().Subscribe(moveChessPieceRequest =>
        {
            var target = ChessGames.FirstOrDefault(i => i.GameInfo.GameId == moveChessPieceRequest.ChessPiece.GameId);
        });
    }

    public List<ChessGame> ChessGames { get; } = new();


    public List<ChessGame> Games { get; set; } = new();

    public void MovePiece(ChessPiece piece, int newX, char newY)
    {
        // Find the piece in the list and update its position
        /*foreach (var chessPiece in _chessPieces)
        {
            if (chessPiece == piece)
            {
                chessPiece.X = newX;
                chessPiece.Y = newY;
                break;
            }
        }*/
    }

    public bool IsMoveValid(ChessPiece piece, int newX, char newY)
    {
        // Check if the move is valid for the given piece
        if (piece.PieceType == ChessPieceType.Pawn)
        {
            // Pawn can only move forward one or two squares on its first move
            if (piece.Y == '2' && newY == '4' && newX == piece.X)
                return true;
            if (newY == piece.Y + 1 && newX == piece.X) return true;
        }
        else if (piece.PieceType == ChessPieceType.Knight)
        {
            // Knight can move in an L shape
            if ((newX == piece.X + 2 && (newY == piece.Y + 1 || newY == piece.Y - 1)) ||
                (newX == piece.X - 2 && (newY == piece.Y + 1 || newY == piece.Y - 1)) ||
                (newY == piece.Y + 2 && (newX == piece.X + 1 || newX == piece.X - 1)) ||
                (newY == piece.Y - 2 && (newX == piece.X + 1 || newX == piece.X - 1)))
                return true;
        }
        else if (piece.PieceType == ChessPieceType.Bishop)
        {
            // Bishop can move diagonally
            if (Math.Abs(newX - piece.X) == Math.Abs(newY - piece.Y)) return true;
        }
        else if (piece.PieceType == ChessPieceType.Rook)
        {
            // Rook can move horizontally or vertically
            if (newX == piece.X || newY == piece.Y) return true;
        }
        else if (piece.PieceType == ChessPieceType.Queen)
        {
            // Queen can move diagonally, horizontally or vertically
            if (Math.Abs(newX - piece.X) == Math.Abs(newY - piece.Y) ||
                newX == piece.X || newY == piece.Y)
                return true;
        }
        else if (piece.PieceType == ChessPieceType.King)
        {
            // King can move one square in any direction
            if (Math.Abs(newX - piece.X) <= 1 && Math.Abs(newY - piece.Y) <= 1) return true;
        }

        return false;
    }

    // Add more methods for game logic as needed
}