using System.Diagnostics;
using ChessFeatureModule.Models;
using NPOI.SS.Formula.Functions;

namespace ChessFeatureModule.Utils;

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


   public static bool IsMoveAllowed(this ChessGame game, BoardPosition startCellId, BoardPosition endCellId)
    {
        var piece = game.ChessPieces.FirstOrDefault(p => p.CellId == startCellId);

       
        if (piece == null || piece.IsCaptured)
            return false;

        if (piece.Color != game.CurrentPlayer.Color)
            return false;

        return piece.Type switch
        {
            ChessPieceType.Pawn => IsPawnMoveAllowed(game,piece, startCellId, endCellId),
            ChessPieceType.Rook => IsRookMoveAllowed(game,piece, startCellId, endCellId),
            ChessPieceType.Knight => IsKnightMoveAllowed(game, piece, startCellId, endCellId),
            ChessPieceType.Bishop => IsBishopMoveAllowed(game,piece, startCellId, endCellId),
            ChessPieceType.Queen => IsQueenMoveAllowed(game,piece, startCellId, endCellId),
            ChessPieceType.King => IsKingMoveAllowed(game,piece, startCellId, endCellId),
            _ => false
        };
    }

 

   private static int ToColumn(this BoardPosition position)
   {
        var col = position.ToString().Substring(0, 1);
        switch (col)
        {
            case "a": return 1;
            case "b": return 2;
            case "c": return 3;
            case "d": return 4;
            case "e": return 5;
            case "f": return 6;
            case "g": return 7;
            case "h": return 8;
        }

        return -1;
   }

   private static int ToRow(this BoardPosition position)
   {
       var row = position.ToString().Substring(1, 1);
       return int.Parse(row);
   }

   private static ChessPiece? GetPieceAt(this ChessGame game, BoardPosition position)
   {
       return game.ChessPieces.FirstOrDefault(p => p.CellId == position);
   }

   private static bool IsPawnMoveAllowed(ChessGame game, ChessPiece pawn, BoardPosition start, BoardPosition end)
   {
     

       int startCol = start.ToColumn();
       int endCol = end.ToColumn();
       int startRow = (int)start.ToRow();
       int endRow = (int)end.ToRow();

       int direction = pawn.Color == ChessPieceColor.White ? 1 : -1;

       // Check for initial two-space move
       if ((pawn.Color == ChessPieceColor.White && startRow == 2) ||
           (pawn.Color == ChessPieceColor.Black && startRow == 7))
       {
           int twoSpaceMoveRow = startRow + (2 * direction);
           if (endCol == startCol && endRow == twoSpaceMoveRow)
           {
                // Check if the destination is empty
                return game.GetPieceAt(end) == null;
            }
       }

       // Check if the pawn is moving forward
       if (endCol == startCol && endRow == startRow + direction)
       {
           // Check if the destination is empty
           return game.GetPieceAt(end) == null;
       }

       // Check if the pawn is capturing a piece diagonally
       if ((endCol == startCol + 1 || endCol == startCol - 1) && endRow == startRow + direction)
       {
           ChessPiece? targetPiece = game.GetPieceAt(end);
           if (targetPiece != null && targetPiece.Color != pawn.Color)
               return true;
       }

       return false;
   }

    


private static bool IsRookMoveAllowed(ChessGame game, ChessPiece rook, BoardPosition startCellId, BoardPosition endCellId)
  {
      // Check if the piece is indeed a rook
      if (rook.Type != ChessPieceType.Rook)
      {
          return false;
      }

      // Rooks move in straight lines, either horizontally or vertically.
      bool isVerticalMove = startCellId.ToColumn() == endCellId.ToColumn();
      bool isHorizontalMove = startCellId.ToRow() == endCellId.ToRow();

      if (!isVerticalMove && !isHorizontalMove)
      {
          return false; // Not a straight line move
      }

      // Check for other pieces in the path (excluding the end cell)
      // Implement the logic to iterate through the cells between startCellId and endCellId
      // and check if any cell contains a piece that is not captured.

      // If the move is to a cell occupied by an opponent's piece, it's allowed (capture).
      // If the move is to an empty cell, it's also allowed.
      // Check the endCellId for these conditions.

      return true; // If all conditions are met, the move is allowed
  }


  private static bool IsKnightMoveAllowed( ChessGame game, ChessPiece knight, BoardPosition startCellId, BoardPosition endCellId)
  {
      // Check if the piece is indeed a knight
      if (knight.Type != ChessPieceType.Knight)
      {
          return false;
      }

      // Calculate the difference in rows and columns between the start and end positions
      int rowDifference = Math.Abs(startCellId.ToRow() - endCellId.ToRow());
      int columnDifference = Math.Abs(startCellId.ToColumn() - endCellId.ToColumn());

      // Knight moves in an L-shape: two squares in one direction and one square perpendicular
      bool isValidKnightMove = (rowDifference == 2 && columnDifference == 1) || (rowDifference == 1 && columnDifference == 2);

      if (!isValidKnightMove)
      {
          return false; // Not a valid L-shaped move
      }

      // Knights can jump over other pieces, so no need to check the path.
      // However, check the conditions at the end cell.
      // The move is allowed if the end cell is either empty or occupied by an opponent's piece for capture.

      return true; // If all conditions are met, the move is allowed
  }


  private static bool IsBishopMoveAllowed( ChessGame game, ChessPiece bishop, BoardPosition startCellId, BoardPosition endCellId)
  {
      // Check if the piece is indeed a bishop
      if (bishop.Type != ChessPieceType.Bishop)
      {
          return false;
      }

      // Calculate the difference in rows and columns between the start and end positions
      int rowDifference = Math.Abs(startCellId.ToRow() - endCellId.ToRow());
      int columnDifference = Math.Abs(startCellId.ToColumn() - endCellId.ToColumn());

      // Bishop moves diagonally, so the absolute difference between rows and columns should be the same
      if (rowDifference != columnDifference)
      {
          return false; // Not a diagonal move
      }

      // Check for other pieces in the path (excluding the end cell)
      // Implement the logic to iterate through the cells between startCellId and endCellId
      // and check if any cell contains a piece that is not captured.

      // If the move is to a cell occupied by an opponent's piece, it's allowed (capture).
      // If the move is to an empty cell, it's also allowed.
      // Check the endCellId for these conditions.

      return true; // If all conditions are met, the move is allowed
  }


  private static bool IsQueenMoveAllowed( ChessGame game, ChessPiece queen, BoardPosition startCellId, BoardPosition endCellId)
  {
      // Check if the piece is indeed a queen
      if (queen.Type != ChessPieceType.Queen)
      {
          return false;
      }

      // Calculate the difference in rows and columns between the start and end positions
      int rowDifference = Math.Abs(startCellId.ToRow() - endCellId.ToRow());
      int columnDifference = Math.Abs(startCellId.ToColumn() - endCellId.ToColumn());

      // Queen moves horizontally, vertically, or diagonally.
      bool isDiagonalMove = rowDifference == columnDifference;
      bool isHorizontalMove = rowDifference == 0 && columnDifference != 0;
      bool isVerticalMove = columnDifference == 0 && rowDifference != 0;

      if (!(isDiagonalMove || isHorizontalMove || isVerticalMove))
      {
          return false; // Not a valid queen move
      }

      // Check for other pieces in the path (excluding the end cell)
      // Implement the logic to iterate through the cells between startCellId and endCellId
      // and check if any cell contains a piece that is not captured.

      // If the move is to a cell occupied by an opponent's piece, it's allowed (capture).
      // If the move is to an empty cell, it's also allowed.
      // Check the endCellId for these conditions.

      return true; // If all conditions are met, the move is allowed
  }


  private static bool IsKingMoveAllowed( ChessGame game, ChessPiece king, BoardPosition startCellId, BoardPosition endCellId)
  {
      // Check if the piece is indeed a king
      if (king.Type != ChessPieceType.King)
      {
          return false;
      }

      // Calculate the difference in rows and columns between the start and end positions
      int rowDifference = Math.Abs(startCellId.ToRow() - endCellId.ToRow());
      int columnDifference = Math.Abs(startCellId.ToColumn() - endCellId.ToColumn());

      // King moves exactly one square in any direction
      bool isSingleSquareMove = (rowDifference <= 1 && columnDifference <= 1) && (rowDifference != 0 || columnDifference != 0);

      if (!isSingleSquareMove)
      {
          return false; // Not a valid single square move
      }

      // Additional checks can be added here for special moves like castling,
      // or checks for moving into check (which is not allowed).

      // Check the conditions at the end cell.
      // The move is allowed if the end cell is either empty or occupied by an opponent's piece for capture.

      return true; // If all conditions are met, the move is allowed
  }


    // Helper methods for movement logic (e.g., IsPathClear, IsWithinBoardBounds, etc.)
    // ...


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