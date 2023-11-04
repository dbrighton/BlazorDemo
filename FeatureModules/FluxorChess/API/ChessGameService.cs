using Dispatcher = Fluxor.Dispatcher;

namespace FluxorChess.API;

public class ChessGameService 
{
  
    private readonly IHubContext<ChessHub> _hub;
    private readonly ILogger<ChessGameService> _Log;
    private readonly IEventAggregator _ea;

    public List<ChessGame> ChessGames { get; private set; } = new();
    public ChessGameService(ILogger<ChessGameService> log, IEventAggregator ea, IHubContext<ChessHub> hub)
    {
        _Log = log;
        _ea = ea;
        _hub = hub;

        _ea.GetEvent<CreateGamePrismEvent>().Subscribe((game) =>
        {
            var target = (from i in ChessGames
                where i.Id == game.Id
                select i).FirstOrDefault();

            if (target == null)
            {
                ChessGames.Add(game);
                _hub.Clients.All.SendAsync(HubConstants.GameListChanged, ChessGames);
            }
            else
            {
                throw new Exception("Game already exists");
            }
            
        });
        _ea.GetEvent<JoinGamePrismEvent>().Subscribe((game) =>
        {
            try
            {
                var target = (from i in ChessGames
                    where i.Id == game.Id
                    select i).FirstOrDefault();

                if (target != null)
                {
                    target.PlayerTwo = game.PlayerTwo;
                    _hub.Clients.All.SendAsync(HubConstants.GameListChanged, ChessGames);
                }
                else
                {
                   
                    game.HubClients?.Caller.SendAsync(HubConstants.GenericError, "Game does not exists");
                }
            }
            catch (Exception e)
            {
                _Log.LogError(e.Message);
            
            }
        });
    
    }

   

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