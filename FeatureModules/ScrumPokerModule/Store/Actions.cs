namespace ScrumPokerFeatureModule.Store;

public record PokerHubStartAction;

public record PokerHubSetConnectedAction(bool Connected);

public record StartGameAction(string Story);

public record OnPlaceBetAction(PokerPlayer Player, int Bet);