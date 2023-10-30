namespace ScrumPokerFeatureModule.Store;

public record PokerState(
    List<PokerPlayer> Players,
    List<ScrumPokerCard> Cards,
    List<ScrumPokerSession> Sessions,
    bool HubConnected);


public class ScrumPokerFeature : Feature<PokerState>
{
    public override string GetName()
    {
        return nameof(PokerState);
    }

    protected override PokerState GetInitialState()
    {
        return new PokerState(new List<PokerPlayer>(), new List<ScrumPokerCard>(), new List<ScrumPokerSession>(), false);
    }
}