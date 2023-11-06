using FluxorChess.Models;

namespace FluxorChess.Utils;

public static class ChessExtensions
{
    public static ChessGame ResetGame(this ChessGame game)
    {
        game.ChessPieces = InitChessBoard(game.GameInfo.GameId);
        game.CapturedChessPieces = new List<ChessPiece>();
        return game;
    }

    public static List<ChessPiece> ResetBoard(this ChessGame game)
    {
        game.CapturedChessPieces = new List<ChessPiece>();
        return InitChessBoard(game.GameInfo.GameId);
    }

    private static List<ChessPiece> InitChessBoard(Guid? gameId)
    {
        var list = new List<ChessPiece>
        {
            //create white first row
            new()
            {
                GameId = gameId,
                CellId = "a1",
                X = 1,
                Y = 'a',
                IsWhite = true,
                PieceType = ChessPieceType.Rook
            },

            new()
            {
                GameId = gameId,
                CellId = "b1",
                X = 1,
                Y = 'b',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            },

            new()
            {
                GameId = gameId,
                CellId = "c1",
                X = 1,
                Y = 'c',
                IsWhite = true,
                PieceType = ChessPieceType.Bishop
            },

            new()
            {
                GameId = gameId,
                CellId = "d1",
                X = 1,
                Y = 'd',
                IsWhite = true,
                PieceType = ChessPieceType.Queen
            },

            new()
            {
                GameId = gameId,
                CellId = "e1",
                X = 1,
                Y = 'e',
                IsWhite = true,
                PieceType = ChessPieceType.King
            },

            new()
            {
                GameId = gameId,
                CellId = "f1",
                X = 1,
                Y = 'f',
                IsWhite = true,
                PieceType = ChessPieceType.Bishop
            },

            new()
            {
                GameId = gameId,
                CellId = "g1",
                X = 1,
                Y = 'g',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            },

            new()
            {
                GameId = gameId,
                CellId = "h1",
                X = 1,
                Y = 'h',
                IsWhite = true,
                PieceType = ChessPieceType.Rook
            }

          
        };

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                GameId = gameId,
                CellId = $"{(char)(i + 96)}2" ,
                X = 2,
                Y = (char)(i + 96),
                IsWhite = true,
                PieceType = ChessPieceType.Pawn
            });

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                GameId = gameId,
                CellId = $"{(char)(i + 96)}7",
                X = 7,
                Y = (char)(i + 96),
                IsWhite = false,
                PieceType = ChessPieceType.Pawn
            });

        //create black first row
        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "a8",
            X = 8,
            Y = 'a',
            IsWhite = false,
            PieceType = ChessPieceType.Rook
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "b8",
            X = 8,
            Y = 'b',
            IsWhite = false,
            PieceType = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "c8",
            X = 8,
            Y = 'c',
            IsWhite = false,
            PieceType = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "d8",
            X = 8,
            Y = 'd',
            IsWhite = false,
            PieceType = ChessPieceType.King
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "e8",
            X = 8,
            Y = 'e',
            IsWhite = false,
            PieceType = ChessPieceType.Queen
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "f8",
            X = 8,
            Y = 'f',
            IsWhite = false,
            PieceType = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "g8",
            X = 8,
            Y = 'g',
            IsWhite = false,
            PieceType = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            GameId = gameId,
            CellId = "h8",
            X = 8,
            Y = 'h',
            IsWhite = false,
            PieceType = ChessPieceType.Rook
        });


        return list;
    }
}