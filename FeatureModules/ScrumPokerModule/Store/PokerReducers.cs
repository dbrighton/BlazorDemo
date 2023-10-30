namespace ScrumPokerFeatureModule.Store;

public static class PokerReducers
{
    [ReducerMethod]
    public static PokerState OnPokerHubSetConnectedAction(PokerState state, PokerHubSetConnectedAction action)
    {
        return state with { HubConnected = action.Connected };
    }

    [ReducerMethod]
    public static PokerState OnPokerSessionsChangedSuccessAction(PokerState state, PokerSessionsChangedSuccessAction action)
    {
        return state with { Sessions = action.Sessions };
    }
}