namespace FluxorChess;




public class StartNewGamePrismEvent:PubSubEvent<ChessPlayer> { }



public class JoinGamePrismEvent : PubSubEvent<JoinGameRequest> { }