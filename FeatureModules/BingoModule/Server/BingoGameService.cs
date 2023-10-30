namespace BingoFeatureModule.Server;

public class BingoGameService : IHostedService
{
    private readonly ILogger<BingoGameService> _logger;


    public BingoGameService(ILogger<BingoGameService> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}