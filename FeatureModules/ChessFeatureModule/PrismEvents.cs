namespace FluxorChess;
public class StartNewGamePrismEvent:PubSubEvent<ChessPlayer> { }
public class JoinGamePrismEvent : PubSubEvent<JoinGameRequest> { }
public class MoveChessPiecePrismEvent : PubSubEvent<Models.ChessGame> { }
public class RefreshGameListPrismEvent : PubSubEvent { }
public class ResignGamePrismEvent : PubSubEvent<Models.ChessGame> { }