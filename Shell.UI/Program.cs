using BingoGameComponents;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add Fluxor
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(
        typeof(Program).Assembly,
        typeof(BingoGameClientComponent).Assembly);
});

await builder.Build().RunAsync();