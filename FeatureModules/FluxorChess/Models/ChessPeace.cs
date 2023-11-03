// Ignore Spelling: Fluxor

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

    private ChessPieceType _pieceType;
    public ChessPieceType PieceType
    {
        get => _pieceType;
        set
        {
            _pieceType = value;
            UpdateImageSrc();
        }
    }

    public string ImageSrc { get; private set; }

    private void UpdateImageSrc()
    {
        var basePath = "./wwwroot/images/";
        var color = IsWhite ? "white" : "black";
        var fileName = $"{color}_{_pieceType.ToString().ToLower()}.svg";
        Console.WriteLine(fileName);
        ImageSrc = $"{basePath}{fileName}";
    }
}
