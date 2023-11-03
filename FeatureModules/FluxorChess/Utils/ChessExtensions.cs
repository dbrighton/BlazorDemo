using FluxorChess.Models;

namespace FluxorChess.Utils;

public static class ChessExtensions
{
    public static ChessGame Reset(this ChessGame game)
    {
        game.ChessPieces = InitChessBoard();
        return game;
    }

    private static List<ChessPiece> InitChessBoard()
    {
        var list = new List<ChessPiece>
        {
            //create white first row
            new()
            {
                X = 1,
                Y = 'a',
                IsWhite = true,
                PieceType = ChessPieceType.Rook
            },

            new()
            {
                X = 1,
                Y = 'b',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            },

            new()
            {
                X = 1,
                Y = 'c',
                IsWhite = true,
                PieceType = ChessPieceType.Bishop
            },

            new()
            {
                X = 1,
                Y = 'd',
                IsWhite = true,
                PieceType = ChessPieceType.Queen
            },

            new()
            {
                X = 1,
                Y = 'e',
                IsWhite = true,
                PieceType = ChessPieceType.King
            },

            new()
            {
                X = 1,
                Y = 'f',
                IsWhite = true,
                PieceType = ChessPieceType.Bishop
            },

            new()
            {
                X = 1,
                Y = 'g',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            },

            new()
            {
                X = 1,
                Y = 'h',
                IsWhite = true,
                PieceType = ChessPieceType.Rook
            },

            new()
            {
                X = 1,
                Y = 'h',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            }
        };

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                X = 2,
                Y = (char)(i + 96),
                IsWhite = true,
                PieceType = ChessPieceType.Pawn
            });

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                X = 7,
                Y = (char)(i + 96),
                IsWhite = false,
                PieceType = ChessPieceType.Pawn
            });

        //create black first row
        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'a',
            IsWhite = false,
            PieceType = ChessPieceType.Rook
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'b',
            IsWhite = false,
            PieceType = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'c',
            IsWhite = false,
            PieceType = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'd',
            IsWhite = false,
            PieceType = ChessPieceType.King
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'e',
            IsWhite = false,
            PieceType = ChessPieceType.Queen
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'f',
            IsWhite = false,
            PieceType = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'g',
            IsWhite = false,
            PieceType = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            X = 8,
            Y = 'h',
            IsWhite = false,
            PieceType = ChessPieceType.Rook
        });


        return list;
    }
}