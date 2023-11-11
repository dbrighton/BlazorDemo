namespace ChessFeatureModule.Models;
public class ChessGame
{
    public GameInfo? GameInfo { get; set; } 
    public ChessPlayer? PlayerOne { get; set; } 
    public ChessPlayer? PlayerTwo { get; set; } 
    public List<ChessPiece> ChessPieces { get; set; } 
    public List<ChessPiece> CapturedChessPieces { get; set; } =new();
    


    public ChessGame()
    {
        GameInfo = new GameInfo
        {
            GameId = Guid.NewGuid()
        };
        ChessPieces = this.ResetBoard();
    }


     [JsonIgnore]
     public IClientProxy? HubClients { get; set; }
}