var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddSignalR(cfg => { cfg.EnableDetailedErrors = true;})
.AddJsonProtocol(opt =>
{
    opt.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.PayloadSerializerOptions.MaxDepth = 50;

}); 


builder.Services.AddFluxor(opt =>
{
#if DEBUG
    opt.ScanAssemblies(
            typeof(UserFeature).Assembly,
            typeof(BingoFeatureModule.BingoFeature).Assembly,
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
builder.Services.AddBlazm();

builder.Services.AddSingleton<ScrumPokerGameService>();
builder.Services.AddSingleton<ScrumPokerHub>();
builder.Services.AddSingleton(new EventAggregator());


var app = builder.Build();
app.Services.GetRequiredService<ScrumPokerGameService>(); // start the singleton service

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapHub<AuthHub>("/authHub");
app.MapHub<BingoHub>("/bingoHub");
app.MapHub<ScrumPokerHub>("/pokerHub");

app.Run();
