namespace UserFeatureModule.Store;

public static class UserReducers
{
    [ReducerMethod]
    public static UserState OnSetConnected(UserState state, AuthHubSetConnectedAction action)
    {
        return state with { HubConnected = action.Connected };
    }
}