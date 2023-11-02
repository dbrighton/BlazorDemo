using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxorPorker.Store;

public record PokerState;

public class PokerFeature : Feature<PokerState>
{
    public override string GetName() => nameof(PokerFeature);
    protected override PokerState GetInitialState() => new PokerState();
}


