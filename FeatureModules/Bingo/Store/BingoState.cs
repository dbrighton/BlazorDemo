namespace BingoFeatureModule.Store;

public record BingoState(List<Person> Players,List<Card> Cards);

public class BingoFeature : Feature<BingoState>
{
    public override string GetName()=>nameof(BingoState);

    protected override BingoState GetInitialState()
    {
       return new BingoState(new List<Person>(),new List<Card>());
    }
}



public static class  BingoReducers
{
    //[ReducerMethod]
    //public static BingoState OnPlaceBetAction(BingoState state, PlaceBetAction action)
    //{
    //    var player = state.Players.FirstOrDefault(p => p.Name == action.PlayerName);
    //    if (player != null)
    //    {
    //        player.Bet = action.Bet;
    //    }
    //    return state with { Players = state.Players };
    //}
}