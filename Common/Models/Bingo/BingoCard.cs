namespace Common.Models.Bingo;

/// <summary>
/// A class representing a Bingo card.
/// </summary>
public class BingoCard
{
    private const int NUM_ROWS = 5;
    private const int NUM_COLS = 5;

    private static readonly Random _random = new Random();

    /// <summary>
    /// Initializes a new instance of the <see cref="BingoCard"/> class.
    /// </summary>
    public BingoCard()
    {
        // Generate an array of unique integers between 1 and 75
        var numbers = Enumerable.Range(1, 75).OrderBy(x => _random.Next()).ToArray();

        Numbers = new int[NUM_ROWS, NUM_COLS];

        // Assign the numbers to the card
        for (int row = 0; row < NUM_ROWS; row++)
        {
            for (int col = 0; col < NUM_COLS; col++)
            {
                Numbers[row, col] = numbers[(col * NUM_ROWS) + row];
            }
        }
    }

    /// <summary>
    /// Gets the numbers on the card.
    /// </summary>
    public int[,] Numbers { get; }

    /// <summary>
    /// Returns a string representation of the Bingo card.
    /// </summary>
    /// <returns>A string representation of the Bingo card.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("+-----+-----+-----+-----+-----+");
        for (int row = 0; row < NUM_ROWS; row++)
        {
            sb.Append("|");
            for (int col = 0; col < NUM_COLS; col++)
            {
                int number = Numbers[row, col];
                sb.Append(number.ToString().PadLeft(2));
                sb.Append(number == 0 ? "F" : " ");
                sb.Append("|");
            }
            sb.AppendLine();
            sb.AppendLine("+-----+-----+-----+-----+-----+");
        }
        return sb.ToString();
    }

    /// <summary>
    /// Checks whether the card contains a specific number.
    /// </summary>
    /// <param name="currentNumber">The number to check for.</param>
    /// <returns>True if the card contains the number, false otherwise.</returns>
    public bool ContainsNumber(int currentNumber)
    {
        for (int row = 0; row < NUM_ROWS; row++)
        {
            for (int col = 0; col < NUM_COLS; col++)
            {
                if (Numbers[row, col] == currentNumber)
                {
                    return true;
                }
            }
        }

        return false;
    }
}