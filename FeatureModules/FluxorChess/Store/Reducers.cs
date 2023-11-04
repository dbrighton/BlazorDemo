namespace FluxorChess.Store;

public static class Reducers
{
    [ReducerMethod]
    public static ChessState OnSetConnected(ChessState state, HubSetConnectedReducerAction reducerAction)
    {
        state.HubConnected = reducerAction.HubConnected;

        return state;
    }

    [ReducerMethod]
    public static ChessState OnChessGameListChangedAction(ChessState state, GameListChangedReducerAction action)
    {
        state.Games = action.Games;

        return state;
    }

    [ReducerMethod]
    public static ChessState OnJoinChessGameSuccessAction(ChessState state, JoinGameReducerAction action)
    {
        state.CurrentGame = action.Game;
        return state;
    }

    [ReducerMethod]
    public static ChessState OnChessPiecesUpdateSuccessAction(ChessState state, ChessPiecesUpdateReducerAction action)
    {
        var game = state.CurrentGame;
        game.ChessPieces = action.ChessPieces.OrderBy(i => i.CellId).ToList();

        return state;
    }

    // [ReducerMethod]
    // public static ChessState OnMoveChessPieceReduceAction(ChessState state, MoveChessPieceReduceAction action)
    // {
    //     var newState = state with
    //     {
    //         /* copy existing state */
    //     };
    //
    //     // Find the piece and update its position in the newState
    //     var piece = newState.CurrentGame.ChessPieces.Find(p => p == action.ChessPiece);
    //     if (piece != null) piece.CellId = action.TargetCellId;
    //
    //     return newState;
    // }

    [ReducerMethod]
    public static ChessState OnGameUpdatedReducerAction(ChessState state, GameUpdatedReducerAction action)
    {
        if (state.CurrentGame != null && state.CurrentGame.Id == action.Game.Id) state.CurrentGame = action.Game;

        var target = (from i in state.Games
            where i.Id == action.Game.Id
            select i).FirstOrDefault();
        if (target != null)
            target = action.Game;
        else
            state.Games.Add(action.Game);

        return state;
    }

    [ReducerMethod]
    public static ChessState OnGameDeletedReducerAction(ChessState state, GameDeletedReducerAction action)
    {
        var target = (from i in state.Games
            where i.Id == action.Game.Id
            select i).FirstOrDefault();
        if (target != null) state.Games.Remove(target);

        if (state.CurrentGame != null && state.CurrentGame.Id == action.Game.Id) state.CurrentGame = null;

        return state;
    }
}