namespace FluxorChess.Actions.Reducer;

public record ChessPiecesUpdateReducerAction(BoardPosition StartCellId, ChessPiece ChessPiece);