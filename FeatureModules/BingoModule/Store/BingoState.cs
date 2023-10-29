using Common.Models.Bingo;

namespace BingoFeatureModule.Store;

public record BingoState(List<Person> Players, List<BingoCard> Cards, bool Connected);
public class BingoFeature : Feature<BingoState>
{
    public override string GetName()=>nameof(BingoState);

    protected override BingoState GetInitialState()
    {
       return new BingoState(new List<Person>(),new List<BingoCard>(),false);
    }
}