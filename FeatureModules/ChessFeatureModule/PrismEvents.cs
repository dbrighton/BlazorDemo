namespace ChessFeatureModule;
public class StartNewGamePrismEvent:PubSubEvent<ChessPlayer> { }
public class JoinGamePrismEvent : PubSubEvent<JoinGameRequest> { }
public class MoveChessPiecePrismEvent : PubSubEvent<ChessGame> { }
public class RefreshGameListPrismEvent : PubSubEvent { }
public class ResignGamePrismEvent : PubSubEvent<ChessGame> { }