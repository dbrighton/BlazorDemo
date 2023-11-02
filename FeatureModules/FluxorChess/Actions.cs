using FluxorChess.Models;

namespace FluxorChess;

public record StartHubAction();
public record HubSetConnectedAction(bool IsHubConnected);
public record ChessSetStateAction(List<ChessGame> Games );


public static class SignalRConstants
{
    public const string GetChessGameSate = "GetChessGameSate";
    public const string ChessGameSateChanged = "ChessGameSateChanged";
}