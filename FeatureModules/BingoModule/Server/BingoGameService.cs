using MediatR;

namespace BingoFeatureModule.Server;

public class BingoGameService:IHostedService
{
    private readonly ILogger<BingoGameService> _logger;
    private readonly IMediator _mediator;

    public BingoGameService(ILogger<BingoGameService> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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

