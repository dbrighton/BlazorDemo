using Common.Models.Poker;

namespace ScrumPokerFeatureModule.Store;

public record PokerState(
    List<Person> Players, 
    List<Card> Cards,
    bool Connected);

public class ScrumPokerFeature : Feature<PokerState>
{
    public override string GetName() => nameof(PokerState);

    protected override PokerState GetInitialState()
    {
        return new PokerState(new List<Person>(), new List<Card>(),false);
    }
}