namespace Bingo.Store;

public record BingoState(List<Person> Players,List<Card> Cards);

public class ScrumPokerFeature : Feature<BingoState>
{
    public override string GetName()=>nameof(BingoState);

    protected override BingoState GetInitialState()
    {
       return new BingoState(new List<Person>(),new List<Card>());
    }
}

public class PlaceBetAction {

    public required string PlayerName { get; set; }
    public int Bet { get; set; }
}

public static class  ScrumPokerReducers
{
    [ReducerMethod]
    public static BingoState OnPlaceBetAction(BingoState state, PlaceBetAction action)
    {
        var player = state.Players.FirstOrDefault(p => p.Name == action.PlayerName);
        if (player != null)
        {
            player.Bet = action.Bet;
        }
        return state with { Players = state.Players };
    }
}