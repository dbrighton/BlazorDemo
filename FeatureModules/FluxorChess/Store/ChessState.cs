
using Fluxor;
using FluxorChess.Models;

namespace FluxorChess.Store;

public record ChessState(List<ChessPiece> CheckPieces);

public class ChessFeature : Feature<ChessState>
{
    public override string GetName()=> nameof(ChessFeature);

    protected override ChessState GetInitialState()
    {
        var list = new List<ChessPiece>
        {
            //create white first row
            new ChessPiece
            {
                X = 1,
                Y = 'a',
                IsWhite = true,
                Type = ChessPieceType.Rook,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'b',
                IsWhite = true,
                Type = ChessPieceType.Knight,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'c',
                IsWhite = true,
                Type = ChessPieceType.Bishop,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'd',
                IsWhite = true,
                Type = ChessPieceType.Queen,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'e',
                IsWhite = true,
                Type = ChessPieceType.King,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'f',
                IsWhite = true,
                Type = ChessPieceType.Bishop,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'g',
                IsWhite = true,
                Type = ChessPieceType.Knight,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'h',
                IsWhite = true,
                Type = ChessPieceType.Rook,
            },

            new ChessPiece
            {
                X = 1,
                Y = 'h',
                IsWhite = true,
                Type = ChessPieceType.Knight,
            },
        };

        for (int i = 1; i <= 8; i++)
        {
            list.Add(new ChessPiece
            {
                X = 2,
                Y = (char)(i + 96),
                IsWhite = true,
                Type = ChessPieceType.Pawn,
            });
        }


        for (int i = 1; i <= 8; i++)
        {
            list.Add(new ChessPiece
            {
                X = 7,
                Y = (char)(i + 96),
                IsWhite = false,
                Type = ChessPieceType.Pawn,
            });
        }






        return new ChessState(list);
    }
}
    

