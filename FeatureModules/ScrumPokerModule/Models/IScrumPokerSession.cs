namespace ScrumPokerFeatureModule.Models;

public interface IScrumPokerSession
{
    Task CreateSessionAsync(IScrumPokerSession session, CancellationToken cancellationToken = default);
    Task AddPlayerAsync(Guid sessionId, PokerPlayer player, CancellationToken cancellationToken = default);
    Task RemovePlayerAsync(Guid sessionId, PokerPlayer player, CancellationToken cancellationToken = default);
    Task UpdateStoryAsync(Guid sessionId, string story, CancellationToken cancellationToken = default);
    Task PlayAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task EndGameAsync(Guid sessionId, CancellationToken cancellationToken = default);
}