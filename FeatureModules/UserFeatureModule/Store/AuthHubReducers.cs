namespace UserFeatureModule.Store;

public static class AuthHubReducers
{
    [ReducerMethod]
    public static HubAuthSate OnSetConnected(HubAuthSate state, AuthHubSetConnectedAction action)
    {
        return state with { Connected = action.Connected };
    }
}