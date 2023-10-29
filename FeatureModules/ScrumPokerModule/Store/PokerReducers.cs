namespace ScrumPokerFeatureModule.Store;

public static class PokerReducers
{
    [ReducerMethod]
    public static PokerState OnPokerHubSetConnectedAction(PokerState state, PokerHubSetConnectedAction action)
    {
        return state with { Connected = action.Connected };
    }
    
}