namespace ScrumPokerFeatureModule.Store;

public record PokerHubStartAction;

public record PokerHubSetConnectedAction(bool Connected);

public record PokerSessionsChangedSuccessAction(List<ScrumPokerGame> Sessions);
public record GetPokerSessionsAction();

public record StartGameAction(string Story);

public record OnPlaceBetAction(PokerPlayer Player, int Bet);