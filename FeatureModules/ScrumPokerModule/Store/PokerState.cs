

namespace ScrumPokerFeatureModule.Store;

public record PokerState(
    List<PokerPlayer> Players, 
    List<ScrumPokerCard> Cards,
    bool HubConnected);

public class ScrumPokerFeature : Feature<PokerState>
{
    public override string GetName() => nameof(PokerState);

    protected override PokerState GetInitialState()
    {
        return new PokerState(new List<PokerPlayer>(), new List<ScrumPokerCard>(),false);
    }
}