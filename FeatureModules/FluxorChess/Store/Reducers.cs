namespace FluxorChess.Store;

public static class Reducers
{
    [ReducerMethod]
    public static ChessState OnSetConnected(ChessState state, HubSetConnectedAction action)
    {
        return state with { HubConnected = action.HubConnected };
    }

    [ReducerMethod]
    public static ChessState OnChessGameListChangedAction(ChessState state, GameListChangedSuccessAction successAction)
    {
        return state with { Games = successAction.Games };
    }

    [ReducerMethod]
    public static ChessState OnJoinChessGameSuccessActionAction(ChessState state, JoinChessGameSuccessAction sction)
    {
        return state with { CurrentGame = sction.Game };
    }

    
}