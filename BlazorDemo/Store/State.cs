using Fluxor;

namespace BlazorDemo.Store;

public record AppState ();

public class AppSateModule: Feature<AppState>{
    public override string GetName()=>nameof(AppState);

    protected override AppState GetInitialState()
    {
        return new AppState();
    }
}