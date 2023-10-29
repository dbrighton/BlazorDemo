namespace UserFeatureModule.Store;

public record UserSate(bool Connected);

public class AuthHubFeature : Feature<UserSate>
{
    public override string GetName() => nameof(UserSate);

    protected override UserSate GetInitialState()
    {
        return new UserSate(false);
    }
}