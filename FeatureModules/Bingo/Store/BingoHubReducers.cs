namespace BingoFeatureModule.Store;

public static class  BingoHubReducers
{
    [ReducerMethod]
    public static BingoState OnSetConnected(BingoState state, BingoHubSetConnectedAction action)
    {
        return state with { Connected = action.Connected };
    }
}