using Common.Models.Poker;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumPoker.Store;

public record PokerState(List<Person> Players, List<Card> Cards);

public class ScrumPokerFeature : Feature<PokerState>
{
    public override string GetName() => nameof(PokerState);

    protected override PokerState GetInitialState()
    {
        return new PokerState(new List<Person>(), new List<Card>());
    }
}



public static class ScrumPokerReducers
{
    [ReducerMethod]
    public static PokerState OnPlaceBetAction(PokerState state, OnPlaceBetAction action)
    {
      
        
        return state;
    }
}