namespace FluxorChess;




public class CreateGamePrismEvent:PubSubEvent<ChessPlayer> { }



public class JoinGamePrismEvent : PubSubEvent<ChessGame> { }