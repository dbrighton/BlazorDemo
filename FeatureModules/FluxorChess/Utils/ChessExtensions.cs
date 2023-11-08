using FluxorChess.Models;

namespace FluxorChess.Utils;

public static class ChessExtensions
{
    public static string ToDisplayName(this ChessPieceType type)
    {
        return type switch
        {
            ChessPieceType.Pawn => "P",
            ChessPieceType.Rook => "R",
            ChessPieceType.Knight => "N",
            ChessPieceType.Bishop => "B",
            ChessPieceType.Queen => "Q",
            ChessPieceType.King => "K",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    public static string PrintGame(this ChessGame game)
    {
        var sb = new StringBuilder();
        sb.AppendLine();

        sb.AppendLine("  +-------------------------------+");
        for (var i = 8; i >= 1; i--)
        {
            sb.Append(i + " | ");
            for (var j = 1; j <= 8; j++)
            {
                var cellId = GetBoardPosition($"{(char)(j + 96)}{i}");
                var piece = game.ChessPieces.FirstOrDefault(x => x.CellId == cellId);
                if (piece != null)
                    sb.Append(piece.Type.ToDisplayName());
                else
                    sb.Append(" ");
                sb.Append(" | ");
            }

            sb.AppendLine();
            sb.AppendLine("  +-------------------------------+");
        }

        sb.AppendLine("    a   b   c   d   e   f   g   h");
        return sb.ToString();
    }
    public static ChessGame ResetGame(this ChessGame game)
    {
        if (game.GameInfo == null)
            return game;

        game.ChessPieces = InitChessBoard(game.GameInfo.GameId);
        game.CapturedChessPieces = new List<ChessPiece>();
        return game;
    }
    public static List<ChessPiece> ResetBoard(this ChessGame game)
    {
        game.CapturedChessPieces = new List<ChessPiece>();
        return InitChessBoard(game.GameInfo.GameId);
    }
    private static List<ChessPiece> InitChessBoard(Guid? gameId)
    {
        var list = new List<ChessPiece>
        {
            //create white first row
            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.a1,
                IsWhite = true,
                Type = ChessPieceType.Rook
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.b1,
                IsWhite = true,
                Type = ChessPieceType.Knight
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.c1,
                IsWhite = true,
                Type = ChessPieceType.Bishop
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.d1,
                IsWhite = true,
                Type = ChessPieceType.Queen
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.e1,
                IsWhite = true,
                Type = ChessPieceType.King
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.f1,
                IsWhite = true,
                Type = ChessPieceType.Bishop
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.g1,
                IsWhite = true,
                Type = ChessPieceType.Knight
            },

            new()
            {
                GameId = gameId,
                CellId =  BoardPosition.h1,
                IsWhite = true,
                Type = ChessPieceType.Rook
            }


        };

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                GameId = gameId,
                CellId = GetBoardPosition($"{(char)(i + 96)}2"),

                IsWhite = true,
                Type = ChessPieceType.Pawn
            });

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                GameId = gameId,
                CellId = GetBoardPosition($"{(char)(i + 96)}7"),
                IsWhite = false,
                Type = ChessPieceType.Pawn
            });

        //create black first row
        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.a8,
            IsWhite = false,
            Type = ChessPieceType.Rook
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.b8,
            IsWhite = false,
            Type = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.c8,
            IsWhite = false,
            Type = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.d8,
            IsWhite = false,
            Type = ChessPieceType.King
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.e8,
            IsWhite = false,
            Type = ChessPieceType.Queen
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.f8,
            IsWhite = false,
            Type = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.g8,
            IsWhite = false,
            Type = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = BoardPosition.h8,
            IsWhite = false,
            Type = ChessPieceType.Rook
        });


        return list;
    }

    public static BoardPosition GetBoardPosition(string cellId)
    {
        if (Enum.TryParse(cellId, true, out BoardPosition position))
        {
            return position;
        }
        else
        {
            throw new ArgumentException("Invalid cell ID", nameof(cellId));
        }
    }

    /// <summary>
    /// Generates a chess board in HTML format from a list of chess pieces
    /// </summary>
    /// <param name="chessPieces"></param>
    /// <returns></returns>
   
  
    private static string GetCellColor(char col, int row)
    {
        bool isEvenRow = row % 2 == 0;
        bool isEvenCol = (col - 'a') % 2 == 0;

        if ((isEvenRow && isEvenCol) || (!isEvenRow && !isEvenCol))
        {
            return "white";
        }
        else
        {
            return "black";
        }
    }
}