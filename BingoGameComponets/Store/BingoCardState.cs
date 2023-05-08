namespace BingoGameComponents.Store
{
    public record BingoCardState(BingoCard BingoCard, List<int> CalledBalls, bool IsWinner);

    public class BingoCardFeature : Feature<BingoCardState>
    {
        public override string GetName() => "BingoCard";
        protected override BingoCardState GetInitialState() => new BingoCardState(new BingoCard(), new List<int>(), false);
    }

    public static class BingoCardReducers
    {
        [ReducerMethod]
        public static BingoCardState UpdateCalledBalls(BingoCardState state, UpdateCalledBallsAction action)
        {
            var calledBalls = new List<int>(state.CalledBalls) { action.Number };
            bool isWinner = state.BingoCard.IsWinner(calledBalls);

            return state with
            {
                CalledBalls = calledBalls,
                IsWinner = isWinner
            };
        }

        [ReducerMethod]
        public static BingoCardState ResetCalledBalls(BingoCardState state, ResetCalledBallsAction action)
        {
            return new BingoCardState(state.BingoCard, new List<int>(), false);
        }
    }

    public static class BingoCardEffects
    {
       
    }

    public record UpdateCalledBallsAction(int Number);
    public record ResetCalledBallsAction();
    public record CheckForBingoAction();
    public record DisplayWinningMessageAction();
}