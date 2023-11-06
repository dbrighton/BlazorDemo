namespace FluxorChess.Models;
public record JoinGameRequest(GameInfo GameInfo, ChessPlayer Player, IClientProxy? HubCaller = null);
public record MoveChessPieceRequest(ChessPiece ChessPiece, string TargetCellId, IClientProxy? HubCaller = null);
