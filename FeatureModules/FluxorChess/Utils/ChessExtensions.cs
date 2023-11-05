using FluxorChess.Models;

namespace FluxorChess.Utils;

public static class ChessExtensions
{
    public static ChessGame ResetGame(this ChessGame game)
    {
        game.ChessPiecesState = InitChessBoard();

        return game;
    }

    public static List<ChessPiece> ResetBoard(this ChessGame game)
    {

        return InitChessBoard();
    }

    private static List<ChessPiece> InitChessBoard()
    {
        var list = new List<ChessPiece>
        {
            //create white first row
            new()
            {
                CellId = "a1",
                X = 1,
                Y = 'a',
                IsWhite = true,
                PieceType = ChessPieceType.Rook
            },

            new()
            {
                CellId = "b1",
                X = 1,
                Y = 'b',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            },

            new()
            {
                CellId = "c1",
                X = 1,
                Y = 'c',
                IsWhite = true,
                PieceType = ChessPieceType.Bishop
            },

            new()
            {
                CellId = "d1",
                X = 1,
                Y = 'd',
                IsWhite = true,
                PieceType = ChessPieceType.Queen
            },

            new()
            {
                CellId = "e1",
                X = 1,
                Y = 'e',
                IsWhite = true,
                PieceType = ChessPieceType.King
            },

            new()
            {
                CellId = "f1",
                X = 1,
                Y = 'f',
                IsWhite = true,
                PieceType = ChessPieceType.Bishop
            },

            new()
            {
                CellId = "g1",
                X = 1,
                Y = 'g',
                IsWhite = true,
                PieceType = ChessPieceType.Knight
            },

            new()
            {
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
                CellId = $"{(char)(i + 96)}2" ,
                X = 2,
                Y = (char)(i + 96),
                IsWhite = true,
                PieceType = ChessPieceType.Pawn
            });

        for (var i = 1; i <= 8; i++)
            list.Add(new ChessPiece
            {
                CellId = $"{(char)(i + 96)}7",
                X = 7,
                Y = (char)(i + 96),
                IsWhite = false,
                PieceType = ChessPieceType.Pawn
            });

        //create black first row
        list.Add(new ChessPiece
        {
            CellId = "a8",
            X = 8,
            Y = 'a',
            IsWhite = false,
            PieceType = ChessPieceType.Rook
        });

        list.Add(new ChessPiece
        {
            CellId = "b8",
            X = 8,
            Y = 'b',
            IsWhite = false,
            PieceType = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            CellId = "c8",
            X = 8,
            Y = 'c',
            IsWhite = false,
            PieceType = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            CellId = "d8",
            X = 8,
            Y = 'd',
            IsWhite = false,
            PieceType = ChessPieceType.King
        });

        list.Add(new ChessPiece
        {
            CellId = "e8",
            X = 8,
            Y = 'e',
            IsWhite = false,
            PieceType = ChessPieceType.Queen
        });

        list.Add(new ChessPiece
        {
            CellId = "f8",
            X = 8,
            Y = 'f',
            IsWhite = false,
            PieceType = ChessPieceType.Bishop
        });

        list.Add(new ChessPiece
        {
            CellId = "g8",
            X = 8,
            Y = 'g',
            IsWhite = false,
            PieceType = ChessPieceType.Knight
        });

        list.Add(new ChessPiece
        {
            CellId = "h8",
            X = 8,
            Y = 'h',
            IsWhite = false,
            PieceType = ChessPieceType.Rook
        });


        return list;
    }
}