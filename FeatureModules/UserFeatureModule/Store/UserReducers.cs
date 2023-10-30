using Microsoft.AspNetCore.Identity;

namespace UserFeatureModule.Store;

public static class UserReducers
{
    [ReducerMethod]
    public static UserState OnSetConnected(UserState state, AuthHubSetConnectedAction action)
    {
        return state with { HubConnected = action.Connected };
    }

    [ReducerMethod]
    public static UserState OnUserLoginSuccessAction(UserState state, UserLoginSuccessAction action)
    {
        var person = new Person
        {
            Name = action.User.Identity.Name,
            email = action.User.Identities.FirstOrDefault().Name
            
        };
        return state with
        {
            User = person,
            IsAuthenticated = action.User.Identities.FirstOrDefault()?.IsAuthenticated
        };
    }

    [ReducerMethod(typeof(UserLogoutSuccessAction))]
    public static UserState OnUserLogoutSuccessAction(UserState state)
    {
        return state with
        {
            User = null,
            IsAuthenticated = false
        };
    }
}