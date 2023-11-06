﻿namespace FluxorChess;

// *************** Effects Actions***************
public record StartHubEffectsAction;
public record HubSetConnectedReducerAction(bool HubConnected);
public record NewGameEffectsAction(ChessPlayer Player);
public record JoinGameEffectsAction(JoinGameRequest JoinGameRequest);
public record MoveChessPieceEffectsAction(ChessGame game);



// *************** Reducers Actions***************
public record GameListChangedReducerAction(List<GameInfo> Games);
public record JoinGameReducerAction(ChessGame Game);
public record NewGameReducerAction(ChessGame Game);
public record GameUpdatedReducerAction(ChessGame Game);
public record GameDeletedReducerAction(ChessGame Game);
public record ChessPiecesUpdateReducerAction(string StartCellId, ChessPiece ChessPiece);


