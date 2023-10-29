using System.Net.NetworkInformation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFluxor(opt =>
{
#if DEBUG
    opt.ScanAssemblies(
            typeof(UserState).Assembly, 
            typeof(BingoState).Assembly, 
            typeof(PokerState).Assembly)
        .UseReduxDevTools();
#else
    opt.ScanAssemblies(
        typeof(BingoState).Assembly,
        typeof(PokerState).Assembly);
#endif
});

builder.Services.AddMatBlazor();
builder.Services.AddMatToaster(cfg =>
{
    cfg.Position = MatToastPosition.BottomRight;
    cfg.MaxDisplayedToasts = 3;
    cfg.PreventDuplicates = true;
    cfg.NewestOnTop = true;
    cfg.ShowCloseButton = true;
    cfg.MaximumOpacity = 100;
    cfg.VisibleStateDuration = 3000;
});

await builder.Build().RunAsync();
