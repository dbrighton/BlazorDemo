
namespace BingoFeatureModule.Models;

/// <summary>
/// Represents a player in a Bingo game.
/// </summary>
public class BingoPlayer : Person
{
    /// <summary>
    /// Gets or sets the Bingo card for the player.
    /// </summary>
    public BingoCard Card { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the player has Bingo.
    /// </summary>
    public bool HasBingo { get; set; }

    /// <summary>
    /// This is the players Bingo card.
    /// </summary>
    public BingoCard BingoCard { get; set; }

    /// <summary>
    /// This is the SignalR connection ID for the player
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// The player's score
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="BingoPlayer"/> class.
    /// </summary>
    public BingoPlayer(BingoCard bingoCard, string connectionId)
    {
        BingoCard = bingoCard;
        ConnectionId = connectionId;
        Card = new BingoCard();
    }
}