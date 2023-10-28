using MediatR;
using Microsoft.Extensions.Logging;

namespace ScrumPokerFeatureModule.Server;

public class ScrumPokerHub:Hub
{
    private readonly ILogger<ScrumPokerHub> _logger;
    private readonly IMediator _mediator;

    public ScrumPokerHub(ILogger<ScrumPokerHub> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
}