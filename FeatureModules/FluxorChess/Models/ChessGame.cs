using Newtonsoft.Json;

namespace FluxorChess.Models;
public class ChessGame
{
    public GameInfo? GameInfo { get; set; } 
    public Person? PlayerOne { get; set; } 
    public Person? PlayerTwo { get; set; } 
    public List<ChessPiece> ChessPiecesState { get; set; } 


    public ChessGame()
    {
        ChessPiecesState = this.ResetBoard();
    }


    [JsonIgnore]
    public IHubCallerClients? HubClients { get; set; }
}