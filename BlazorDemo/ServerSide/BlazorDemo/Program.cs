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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapHub<BingoHub>("/bingoHub");
app.MapHub<ScrumPokerHub>("/pokerHub");

app.Run();
