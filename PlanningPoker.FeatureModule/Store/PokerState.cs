using Common.Models.Poker;
using Fluxor;

namespace PlanningPoker.FeatureModule.Store;

public record PokerState(List<Card> Cards, List<Participant> Participants, string CurrentStory, bool IsVotingInProgress) { }

public class PlanningPokerFeature : Feature<PokerState>
{
    public override string GetName() => nameof(PlanningPokerFeature);

    protected override PokerState GetInitialState()
    {
        return new PokerState(
            new List<Card>(),
            new List<Participant>(),
            string.Empty,
            false);
    }
}