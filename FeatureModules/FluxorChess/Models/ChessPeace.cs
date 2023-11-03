using System.ComponentModel.DataAnnotations;

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

public class ChessGame
{
    [Display(Name = "Create By")] public string CreateBy { get; set; }

    public Person PlayerOne { get; set; } = new();
    public Person PlayerTwo { get; set; } = new();
    public List<ChessPiece> ChessPieces { get; set; } = new();

    [Display(Name = "Last Updated")] public DateTime LastUpdateTimestamp { get; set; } = DateTime.UtcNow;
}