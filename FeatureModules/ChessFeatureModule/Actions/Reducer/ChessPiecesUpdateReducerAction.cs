namespace ChessFeatureModule.Actions.Reducer;

public record ChessPiecesUpdateReducerAction(BoardPosition StartCellId, ChessPiece ChessPiece);