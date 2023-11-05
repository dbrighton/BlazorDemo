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
        game.ChessPiecesState = action.ChessPieces.OrderBy(i => i.CellId).ToList();

        return state;
    }
    
    [ReducerMethod]
    public static ChessState OnGameDeletedReducerAction(ChessState state, GameDeletedReducerAction action)
    {
        state.Games.RemoveAll(i => i.GameId == action?.Game?.GameInfo?.GameId);


        if (state.CurrentGame != null) state.CurrentGame = null;

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    [ReducerMethod]
    public static ChessState OnGameCreatedReducerAction(ChessState state, GameCreatedReducerAction action)
    {
        state.CurrentGame = action.Game;

        return state;
    }
}