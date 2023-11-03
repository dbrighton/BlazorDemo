﻿namespace ServerSide6WithFluxor.Store;

public record AppSate
{
}

public class AppStateFeature : Feature<AppSate>
{
    public override string GetName()
    {
        return nameof(AppStateFeature);
    }

    protected override AppSate GetInitialState()
    {
        return new AppSate();
    }
}