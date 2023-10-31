namespace ScrumPokerFeatureModule.Store;

public record PokerHubStartAction;

public record PokerHubSetConnectedAction(bool Connected);

public record PokerSessionChangedSuccessAction(ScrumPokerSession Session);
public record GetPokerSessionsAction();
public record NewGameAction(Person ScrumMaster,string StoryName,string Story);
public record NewGameSuccessAction(ScrumPokerSession Session);



public record OnPlaceBetAction(PokerPlayer Player, int Bet);