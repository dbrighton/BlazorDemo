namespace UserFeatureModule.Store;

public record HubAuthSate(bool Connected);

public class AuthHubFeature : Feature<HubAuthSate>
{
    public override string GetName() => nameof(HubAuthSate);

    protected override HubAuthSate GetInitialState()
    {
        return new HubAuthSate(false);
    }
}