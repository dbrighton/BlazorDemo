namespace BingoFeatureModule.Store;

public static class  BingoReducers
{
    [ReducerMethod]
    public static BingoState OnBingoHubSetConnectedAction(BingoState state, BingoHubSetConnectedAction action)
    {
        return state with { HubConnected = action.Connected };
    }
}