using FluxorChess.Models;

namespace FluxorChess;

public record StartHubAction;

public record HubSetConnectedAction(bool IsHubConnected);

public record StartGameAction(string PlayerOne);

public record ChessSetStateAction(List<ChessGame> Games);

public static class SignalRConstants
{
    public const string GetChessGameSate = "GetChessGameSate";
    public const string CreateGame = "ChessGameSateChanged";
    public const string ChessGameSateChanged = "ChessGameSateChanged";
}