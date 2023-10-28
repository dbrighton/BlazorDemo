using BingoFeatureModule;
using BingoFeatureModule.Server;

namespace Bingo.Store;

public class BingoGame : GameBase<BingoHub>
{
    public BingoGame(IHubContext<BingoHub> hubContext) : base(hubContext)
    {
    }
}
