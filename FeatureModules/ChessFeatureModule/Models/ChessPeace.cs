// This file contains the ChessPiece class and related enums and properties.

namespace ChessFeatureModule.Models;

// Enum representing the positions on the chess board.
public enum BoardPosition
{
    a8, b8, c8, d8, e8, f8, g8, h8,
    a7, b7, c7, d7, e7, f7, g7, h7,
    a6, b6, c6, d6, e6, f6, g6, h6,
    a5, b5, c5, d5, e5, f5, g5, h5,
    a4, b4, c4, d4, e4, f4, g4, h4,
    a3, b3, c3, d3, e3, f3, g3, h3,
    a2, b2, c2, d2, e2, f2, g2, h2,
    a1, b1, c1, d1, e1, f1, g1, h1
}

// Enum representing the types of chess pieces.
public enum ChessPieceType
{
    [Display(Name = "P")] Pawn,
    [Display(Name = "R")] Rook,
    [Display(Name = "K")] Knight,
    [Display(Name = "B")] Bishop,
    [Display(Name = "Q")] Queen,
    [Display(Name = "K")] King,
    None
}

// Class representing a chess piece.
public class ChessPiece
{
    // Property representing the color of the chess piece.
    public ChessPieceColor Color
    {
        get
        {
            if (IsWhite)
                return ChessPieceColor.White;
            return ChessPieceColor.Black;
        }
    }

    // Property representing the ID of the game the chess piece belongs to.
    public Guid? GameId { get; set; }

    // Property representing the source of the image of the chess piece.
    public string? ImageSrc { get; private set; }

    // Property representing the position of the chess piece on the board.
    public BoardPosition CellId { get; set; }

    // Property representing whether the chess piece has been captured.
    public bool IsCaptured { get; set; } = false;

    // Property representing whether the chess piece is white.
    public bool IsWhite { get; set; }

    // Property representing the type of the chess piece.
    private ChessPieceType _type;

    public ChessPieceType Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdateImageSrc();
        }
    }

    // Property representing the row of the chess piece on the board.
    public int Row => int.Parse(CellId.ToString().Substring(1, 1));

    // Property representing the column of the chess piece on the board.
    public int Column => CellId.ToString().Substring(0, 1) switch
    {
        "a" => 1,
        "b" => 2,
        "c" => 3,
        "d" => 4,
        "e" => 5,
        "f" => 6,
        "g" => 7,
        "h" => 8,
        _ => throw new ArgumentException("Invalid column value")
    };

    public bool IsFirstMove { get; set; } = true;

    // Method to update the source of the image of the chess piece.
    private void UpdateImageSrc()
    {
        var basePath = "_content/ChessFeatureModule/Images/";
        var color = IsWhite ? "white" : "black";
        var fileName = $"{color}_{_type.ToString().ToLower()}.webp";
        ImageSrc = $"{basePath}{fileName}";
    }
}