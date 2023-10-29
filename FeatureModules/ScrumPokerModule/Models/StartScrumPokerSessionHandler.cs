namespace ScrumPokerFeatureModule.Models;

public class StartScrumPokerSessionHandler : IRequestHandler<StartScrumPokerSessionRequest, Guid>
{
    private readonly IScrumPokerSession _session;

    public StartScrumPokerSessionHandler(IScrumPokerSession session)
    {
        _session = session;
    }

    public async Task<Guid> Handle(StartScrumPokerSessionRequest request, CancellationToken cancellationToken)
    {
        var sessionId = Guid.NewGuid();
        _session.CreateSessionAsync(request.Session, cancellationToken);
        return sessionId;
    }
}