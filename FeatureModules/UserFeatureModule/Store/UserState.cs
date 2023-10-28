namespace UserFeatureModule.Store;

public record UserState(Person User, bool IsLoggedIn);

partial class UserFeature : Feature<UserState>
{
    public override string GetName() => nameof(UserState);

    protected override UserState GetInitialState()
    {
        return new UserState(new Person(), false);
    }
}

public static class UserReducers
{
    [ReducerMethod]
    public static UserState OnLoginAction(UserState state, OnLoginAction action)
    {
        return state with { User = action.User, IsLoggedIn = true };
    }

    [ReducerMethod]
    public static UserState OnLogoutAction(UserState state, OnLogoutAction action)
    {
        return state with { User = new Person(), IsLoggedIn = false };
    }
}