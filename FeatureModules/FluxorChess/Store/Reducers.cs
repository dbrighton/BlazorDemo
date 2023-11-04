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
        game.ChessPieces = action.ChessPieces;

        return state;
    }


}