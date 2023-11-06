namespace FluxorChess.Models;
public class ChessGame
{
    public GameInfo? GameInfo { get; set; } 
    public Person? PlayerOne { get; set; } 
    public Person? PlayerTwo { get; set; } 
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