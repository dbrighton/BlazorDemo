namespace FluxorChess;

public record StartHubAction;

public record HubSetConnectedAction(bool HubConnected);

public record ChessNewGameAction(ChessPlayer Player);

public record GameListChangedSuccessAction(List<ChessGame> Games);

public record AddChessGameSignalRAction(ChessGame Game);