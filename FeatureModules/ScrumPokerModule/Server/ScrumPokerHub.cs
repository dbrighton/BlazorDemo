using MediatR;

namespace ScrumPokerFeatureModule.Server;

public class ScrumPokerHub : Hub
{
    private readonly ILogger<ScrumPokerHub> _logger;
    private readonly IMediator _mediator;

    public ScrumPokerHub(ILogger<ScrumPokerHub> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HubMethodName(Constants.GetPokerSessions)]
    public async Task GetPokerSessions()
    {
        _logger.LogInformation("GetPokerSessions");
        var sessions = await _mediator.Send(new GetPokerSessionsQuery());
        await Clients.Caller.SendAsync(Constants.GetPokerSessions, sessions);
    }
}