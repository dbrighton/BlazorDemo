﻿using ScrumPokerFeatureModule;

namespace ScrumPoker.Store;

public class ScrumPokerGame : GameBase<ScrumPokerHub>
{
    public ScrumPokerGame(IHubContext<ScrumPokerHub> hubContext) : base(hubContext)
    {
    }
}