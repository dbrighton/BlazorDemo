namespace FluxorChess.Models;
public class ChessGame
{
    public Guid Id { get;  } = Guid.NewGuid();
    [Display(Name = "Create By")] public string CreateBy { get; set; }=string.Empty;

    public Person PlayerOne { get; set; } = new();
    public Person PlayerTwo { get; set; } = new();
    public List<ChessPiece> ChessPieces { get; set; } = new();

    [Display(Name = "Last Updated")] public DateTime LastUpdateTimestamp { get; set; } = DateTime.UtcNow;
    
}