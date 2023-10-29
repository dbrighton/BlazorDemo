namespace UserFeatureModule.Store;

public static class AuthReducers
{
    [ReducerMethod]
    public static UserSate OnSetConnected(UserSate state, AuthHubSetConnectedAction action)
    {
        return state with { Connected = action.Connected };
    }
}