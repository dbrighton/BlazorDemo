using ScrumPokerFeatureModule.Server;

namespace ScrumPokerFeatureModule.Store;

public class ScrumPokerGame : GameBase<ScrumPokerHub>
{
    public ScrumPokerGame(IHubContext<ScrumPokerHub> hubContext) : base(hubContext)
    {
    }
}
