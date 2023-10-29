namespace UserFeatureModule.Store;

public record UserState(bool HubConnected,bool IsLoggedIn );


public class AuthHubFeature : Feature<UserState>
{
    public override string GetName() => nameof(UserState);

    protected override UserState GetInitialState()
    {
        return new UserState(false,false);
    }
}