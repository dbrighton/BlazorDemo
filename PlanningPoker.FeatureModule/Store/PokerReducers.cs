using Fluxor;

namespace PlanningPoker.FeatureModule.Store;


using Fluxor;

public static class PokerReducers
{
    [ReducerMethod]
    public static PokerState ReduceStartGameAction(PokerState state, StartGameAction action) =>
        state with { IsVotingInProgress = true };

    [ReducerMethod]
    public static PokerState ReduceEndGameAction(PokerState state, EndGameAction action) =>
        state with { IsVotingInProgress = false };

    [ReducerMethod]
    public static PokerState ReduceVoteAction(PokerState state, VoteAction action) =>
        state with
        {
            Participants = state.Participants.Select(participant =>
                participant.ParticipantName == action.ParticipantName
                    ? participant with { /* Update participant properties as needed */ }
                    : participant
            ).ToList()
        };
}


public class StartGameAction
{
    // You can include any necessary properties or data with this action.
}

public class EndGameAction
{
    // You can include any necessary properties or data with this action.
}

public class VoteAction
{
    public string ParticipantName { get; }
    public int VoteValue { get; }

    public VoteAction(string participantName, int voteValue)
    {
        ParticipantName = participantName;
        VoteValue = voteValue;
    }
}

