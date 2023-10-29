using MatBlazor;
using BingoFeature = BingoFeatureModule.BingoFeature;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(ScrumPokerGameService).Assembly,
        typeof(BingoGameService).Assembly);
});


builder.Services.AddFluxor(opt =>
{
#if DEBUG
    opt.ScanAssemblies(
            typeof(UserFeature).Assembly,
            typeof(BingoFeature).Assembly,
            typeof(ScrumPokerFeature).Assembly)
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapHub<AuthHub>("/authHub");
app.MapHub<BingoHub>("/bingoHub");
app.MapHub<ScrumPokerHub>("/pokerHub");

app.Run();