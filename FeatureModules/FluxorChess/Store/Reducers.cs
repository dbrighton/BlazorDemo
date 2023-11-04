namespace FluxorChess.Store;

public static class Reducers
{
    [ReducerMethod]
    public static ChessState OnSetConnected(ChessState state, HubSetConnectedAction action)
    {
        return state with { HubConnected = action.HubConnected };
    }

    [ReducerMethod]
    public static ChessState OnChessGameListChangedAction(ChessState state, GameListChangedSuccessAction action)
    {
        return state with { Games = action.Games };
    }

    [ReducerMethod]
    public static ChessState OnJoinChessGameSuccessAction(ChessState state, JoinChessGameSuccessAction action)
    {
        return state with { CurrentGame = action.Game };
    }

    [ReducerMethod]
    public static ChessState OnChessPiecesUpdateSuccessAction(ChessState state, ChessPiecesUpdateSuccess action)
    {
        var game = state.CurrentGame;
        game.ChessPieces = (action.ChessPieces.OrderBy(i => i.CellId)).ToList();

        return state;
    }

    [ReducerMethod]
    public static ChessState OnMoveChessPieceReduceAction(ChessState state, MoveChessPieceReduceAction action)
    {
        var newState = state with { /* copy existing state */ };

        // Find the piece and update its position in the newState
        var piece = newState.CurrentGame.ChessPieces.Find(p => p == action.ChessPiece);
        if (piece != null)
        {
            piece.CellId = action.TargetCellId;
        }

        return newState;
    }


}