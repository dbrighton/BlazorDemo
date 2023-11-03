namespace UserFeatureModule.Store;

public record UserState(bool HubConnected, Person? User, bool? IsAuthenticated);

public class AuthHubFeature : Feature<UserState>
{
    public override string GetName()
    {
        return nameof(UserState);
    }

    protected override UserState GetInitialState()
    {
        return new UserState(false, null, null);
    }
}