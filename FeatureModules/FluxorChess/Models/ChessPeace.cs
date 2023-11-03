namespace FluxorChess.Models;

public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public class ChessPiece
{
    public bool IsDead { get; set; } = false;
    public int X { get; set; }
    public char Y { get; set; }
    public bool IsWhite { get; set; }
    public bool IsAlive { get; set; } = true;
    public ChessPieceType PieceType { get; set; }
}