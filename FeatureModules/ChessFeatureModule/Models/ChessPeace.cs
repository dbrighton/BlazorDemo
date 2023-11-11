

namespace ChessFeatureModule.Models;


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

public enum ChessPieceType
{
    [Display(Name = "P")]
    Pawn,
    [Display(Name = "R")]
    Rook,
    [Display(Name = "K")]
    Knight,
    [Display(Name = "B")]
    Bishop,
    [Display(Name = "Q")]
    Queen,
    [Display(Name = "K")]
    King,
    None
}

public class ChessPiece
{
    public ChessPieceColor Color {
        get
        {
            if (IsWhite)
            {
                return ChessPieceColor.White;
            }
            else
            {
                return ChessPieceColor.Black;
            }
        }
    }
    public Guid? GameId { get; set; }
    public string? ImageSrc { get; private set; }
    public BoardPosition CellId { get; set; } 
    public bool IsCaptured { get; set; } = false;
    public bool IsWhite { get; set; }

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



    public int Row => int.Parse(CellId.ToString().Substring(1, 1));
    public char? Column => CellId.ToString().Substring(0, 1).FirstOrDefault();

    private void UpdateImageSrc()
    {

        var basePath = "_content/ChessFeatureModule/Images/";
        var color = IsWhite ? "white" : "black";
        var fileName = $"{color}_{_type.ToString().ToLower()}.svg";
        ImageSrc = $"{basePath}{fileName}";
    }

    
}
