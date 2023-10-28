namespace ScrumPokerFeatureModule.Models;

public class StartScrumPokerSessionHandler : IRequestHandler<StartScrumPokerSessionRequest, Guid>
{
    private readonly IScrumPokerSessionStore _sessionStore;

    public StartScrumPokerSessionHandler(IScrumPokerSessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }

    public async Task<Guid> Handle(StartScrumPokerSessionRequest request, CancellationToken cancellationToken)
    {
        var sessionId = Guid.NewGuid();
        _sessionStore.CreateSession(sessionId);
        return sessionId;
    }
}