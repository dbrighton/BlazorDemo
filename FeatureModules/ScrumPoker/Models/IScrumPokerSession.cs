namespace ScrumPokerFeatureModule.Models;

public  interface IScrumPokerSession
{
    Task CreateSessionAsync(Guid sessionId);
}