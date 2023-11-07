namespace UserFeatureModule.Store;

public record UserState(bool HubConnected, Person? User, bool? IsAuthenticated);

public class UserFeature : Feature<UserState>
{
    public override string GetName()
    {
        return nameof(UserFeature);
    }

    protected override UserState GetInitialState()
    {
        return new UserState(false, null, null);
    }
}