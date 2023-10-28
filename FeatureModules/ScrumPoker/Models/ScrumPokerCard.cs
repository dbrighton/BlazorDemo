namespace ScrumPokerFeatureModule.Models;

public class ScrumPokerCard
{
    public int Value { get; set; }
    public string Description { get; set; }
}

public class StartScrumPokerSessionRequest : IRequest<Guid>
{
    public IScrumPokerSession Session { get; set; }
}