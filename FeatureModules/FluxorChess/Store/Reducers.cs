
namespace FluxorChess.Store;

public static class Reducers
{
    [ReducerMethod]
    public static ChessState OnHubSetConnected(ChessState state, HubSetConnectedReducerAction reducerAction)
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
        if (state.CurrentGame == null)
            return state;

      
        return state;
    }

    [ReducerMethod]
    public static ChessState OnGameDeletedReducerAction(ChessState state, GameDeletedReducerAction action)
    {
        state.Games.RemoveAll(i => i.GameId == action?.Game?.GameInfo?.GameId);
        state.CurrentGame = null;

        return state ?? throw new ArgumentNullException(nameof(state));
    }

    [ReducerMethod]
    public static ChessState OnGameCreatedReducerAction(ChessState state, NewGameReducerAction action)
    {
        state.CurrentGame = action.Game;
        return state;
    }
}

