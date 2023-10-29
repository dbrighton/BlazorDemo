
namespace BingoFeatureModule.Store;

public record BingoHubStartAction();

public record BingoHubSetConnectedAction(bool Connected);

public record StartJoinGameAction(Person Player);
