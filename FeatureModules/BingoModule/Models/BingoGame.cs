namespace BingoFeatureModule.Models;

/// <summary>
///     Represents a Bingo game.
/// </summary>
public class BingoGame
{
    private readonly BingoHub _bingoHub;
    private readonly Cage _cage;
    private readonly List<BingoCard> _cards;
    private readonly IMediator _mediator;

    private readonly List<BingoPlayer> _players;
    private int _currentNumber;
    private bool _isRunning;
    private int _nextNumber;

    /// <summary>
    ///     Creates a new instance of the <see cref="BingoGame" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{BingoGame}" /> used for logging.</param>
    /// <param name="bingoHub">An instance of <see cref="BingoHub" /> used for broadcasting Bingo events to connected clients.</param>
    /// <param name="mediator">An instance of <see cref="IMediator" /> used for sending and publishing MediatR messages.</param>
    public BingoGame(BingoHub bingoHub, IMediator mediator)
    {
        _bingoHub = bingoHub;
        _mediator = mediator;

        _players = new List<BingoPlayer>();
        _cards = new List<BingoCard>();
        _cage = new Cage();
        _nextNumber = 1;
    }


    /// <summary>
    ///     Adds a player to the Bingo game.
    /// </summary>
    /// <param name="player">The player to add.</param>
    public void AddPlayer(BingoPlayer player)
    {
        _players.Add(player);
    }

    /// <summary>
    ///     Removes a player from the Bingo game.
    /// </summary>
    /// <param name="player">The player to remove.</param>
    public void RemovePlayer(BingoPlayer player)
    {
        _players.Remove(player);
    }

    /// <summary>
    ///     Adds a card to the Bingo game.
    /// </summary>
    /// <param name="card">The card to add.</param>
    public void AddCard(BingoCard card)
    {
        _cards.Add(card);
    }

    /// <summary>
    ///     Removes a card from the Bingo game.
    /// </summary>
    /// <param name="card">The card to remove.</param>
    public void RemoveCard(BingoCard card)
    {
        _cards.Remove(card);
    }

    /// <summary>
    ///     Starts the Bingo game by continuously drawing numbers from the Cage and checking each player's Bingo card for
    ///     matches.
    /// </summary>
    /// <param name="delayMilliseconds">The delay in milliseconds between each number draw.</param>
    public async Task Start(int delayMilliseconds)
    {
    }
}