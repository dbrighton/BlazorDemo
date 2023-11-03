namespace FluxorChess.Store;

/// <summary>
///     Represents the state of the chess game.
/// </summary>
/// <remarks>
///     This class is used to store the current state of the chess game.
/// </remarks>
public record ChessState(List<ChessGame> Games, ChessGame CurrentGame, bool HubConnected = false);

public class ChessFeature : Feature<ChessState>
{
    public override string GetName()
    {
        return nameof(ChessFeature);
    }

    /// <summary>
    ///     Gets the initial state of the chess game.
    /// </summary>
    /// <returns>The initial state of the chess game.</returns>
    /// <remarks>
    ///     This method is used to initialize the state of the chess game.
    /// </remarks>
    protected override ChessState GetInitialState()
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

        return new ChessState(new List<ChessGame>(), null);
    }
}