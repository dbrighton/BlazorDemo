namespace ChessFeatureModule.Models;

public class ChessPlayer : Person
{
    public ChessPieceColor Color { get; set; } = ChessPieceColor.White;
}