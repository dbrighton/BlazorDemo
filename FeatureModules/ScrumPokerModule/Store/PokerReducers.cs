namespace ScrumPokerFeatureModule.Store;

public static class PokerReducers
{
    [ReducerMethod]
    public static PokerState OnPokerHubSetConnectedAction(PokerState state, PokerHubSetConnectedAction action)
    {
        return state with { HubConnected = action.Connected };
    }

    [ReducerMethod]
    public static PokerState OnPokerSessionsChangedSuccessAction(PokerState state, PokerSessionChangedSuccessAction action)
    {
        var target = (from i in state.Sessions
            where i.Id == action.Session.Id
            select i).FirstOrDefault();

        if (target != null)
        {
            state.Sessions.Remove(target);
        }
        state.Sessions.Add(action.Session);

        return state;
    }
}