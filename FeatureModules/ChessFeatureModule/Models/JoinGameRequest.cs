﻿namespace ChessFeatureModule.Models;
public record JoinGameRequest(GameInfo GameInfo, ChessPlayer Player, IClientProxy? HubCaller = null);
