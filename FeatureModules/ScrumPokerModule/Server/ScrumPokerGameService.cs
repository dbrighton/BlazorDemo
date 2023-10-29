using MediatR;
using Microsoft.Extensions.Hosting;

namespace ScrumPokerFeatureModule.Server;

public class ScrumPokerGameService : IHostedService
{
    private readonly ILogger<ScrumPokerGameService> _logger;
    private readonly IMediator _mediator;

    public ScrumPokerGameService(ILogger<ScrumPokerGameService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    private List<IScrumPokerSession> Sessions { get; } = new();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task StartAsync(IScrumPokerSession scrumPokerSession, CancellationToken cancellationToken)
    {
        // Check if cancellation is requested
        if (cancellationToken.IsCancellationRequested)
            // Handle cancellation if requested (e.g., cleanup or exit)
            return;


        // Check if the session already exists in Sessions
        if (!Sessions.Contains(scrumPokerSession))
        {
            // Add the session to Sessions
            Sessions.Add(scrumPokerSession);
            await scrumPokerSession.CreateSessionAsync(scrumPokerSession, cancellationToken);
        }
    }
}