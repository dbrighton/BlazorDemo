using Fluxor;

namespace PlanningPoker.FeatureModule;

public static class PokerReducers
{
    [ReducerMethod]
    public static PokerState ReduceStartGameAction(PokerState state, StartGameAction action)
    {
        // In response to a StartGameAction, you might initialize the state as needed.
        // For example, you can set IsVotingInProgress to true and clear previous votes.
        return new PokerState(state.Cards, state.Participants, state.CurrentStory, true);
    }

    [ReducerMethod]
    public static PokerState ReduceEndGameAction(PokerState state, EndGameAction action)
    {
        // Handle the EndGameAction, update the state accordingly.
        // For example, you can set IsVotingInProgress to false.
        return new PokerState(state.Cards, state.Participants, state.CurrentStory, false);
    }

    // You can create more reducer methods to handle various actions in your game.

    // Example of a reducer to update participant votes:
    [ReducerMethod]
    public static PokerState ReduceVoteAction(PokerState state, VoteAction action)
    {
        // You would update the participant's vote in response to this action.
        // You'll need to identify the participant and story to update.
        // This is just a simplified example.
        var updatedParticipants = state.Participants.Select(participant =>
        {
            if (participant.Name == action.ParticipantName)
            {
                return new PokerState.Participant
                {
                    Name = participant.Name,
                    // Update the participant's vote here.
                };
            }
            return participant;
        }).ToList();

        return new PokerState(state.Cards, updatedParticipants, state.CurrentStory, state.IsVotingInProgress);
        
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

// You can create more actions as needed for your game, such as actions for managing participants, stories, etc.

}