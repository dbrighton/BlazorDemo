using FluxorChess.Models;

namespace FluxorChess.API;

public class ChessGameService
{
    private readonly List<ChessPiece> _chessPieces;

    public ChessGameService(List<ChessPiece> chessPieces)
    {
        _chessPieces = chessPieces;
    }

    public void MovePiece(ChessPiece piece, int newX, char newY)
    {
        // Find the piece in the list and update its position
        foreach (var chessPiece in _chessPieces)
            if (chessPiece == piece)
            {
                chessPiece.X = newX;
                chessPiece.Y = newY;
                break;
            }
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