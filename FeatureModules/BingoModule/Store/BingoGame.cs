using BingoFeatureModule.Server;

namespace BingoFeatureModule.Store;

public class BingoGame : GameBase<BingoHub>
{
    public BingoGame(IHubContext<BingoHub> hubContext) : base(hubContext)
    {
    }
}
