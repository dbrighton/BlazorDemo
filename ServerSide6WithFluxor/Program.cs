using Blazm.Components;
using FluxorChess.API;
using Microsoft.AspNetCore.Builder;
using UserFeatureModule.API;
using UserFeatureModule.Store;

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
builder.Services.AddSingleton<WeatherForecastService>();


builder.Services.AddSignalR();

builder.Services.AddFluxor(
    o => o.ScanAssemblies(
            typeof(Program).Assembly, 
            typeof(AuthHubFeature).Assembly, 
            typeof(ChessFeature).Assembly)
        .UseReduxDevTools()
);

builder.Services.AddBlazm();

builder.Services.AddMatBlazor();
builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.BottomFullWidth;
    config.PreventDuplicates = false;
    config.NewestOnTop = true;
    config.ShowCloseButton = false;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 9000;
});

var app = builder.Build();

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
app.MapHub<ChessHub>("/chessHub");

app.Run();
