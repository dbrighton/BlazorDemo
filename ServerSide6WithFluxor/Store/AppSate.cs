namespace ServerSide6WithFluxor.Store;

public record AppSate
{
}

public class AppStateFeature : Feature<AppSate>
{
    public override string GetName() => nameof(AppStateFeature);
    protected override AppSate GetInitialState() => new();
}
