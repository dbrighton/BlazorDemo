namespace BingoFeatureModule.Models;

/// <summary>
///     A class representing a Bingo card.
/// </summary>
public class BingoCard
{
    private const int NUM_ROWS = 5;
    private const int NUM_COLS = 5;

    private static readonly Random _random = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="BingoCard" /> class.
    /// </summary>
    public BingoCard()
    {
        Numbers = GenerateBingoCard();
    }

    /// <summary>
    ///     Gets the numbers on the card.
    /// </summary>
    public int[,] Numbers { get; }

    public string Id { get; set; }

    /// <summary>
    ///     Generates a Bingo card with unique numbers within the range of 1 to 75.
    /// </summary>
    /// <returns>The generated Bingo card.</returns>
    private static int[,] GenerateBingoCard()
    {
        var numbers = GenerateNumbers();

        var card = new int[NUM_ROWS, NUM_COLS];

        // Assign the numbers to the card
        for (var row = 0; row < NUM_ROWS; row++)
        for (var col = 0; col < NUM_COLS; col++)
            card[row, col] = numbers[col * NUM_ROWS + row];

        return card;
    }

    /// <summary>
    ///     Generates an array of unique integers between 1 and 75.
    /// </summary>
    /// <returns>An array of unique integers between 1 and 75.</returns>
    private static int[] GenerateNumbers()
    {
        var numbers = Enumerable.Range(1, 75).OrderBy(x => _random.Next()).ToArray();
        return numbers;
    }

    /// <summary>
    ///     Returns a string representation of the Bingo card in ASCII art format.
    /// </summary>
    /// <returns>A string representation of the Bingo card.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("+-----+-----+-----+-----+-----+");

        for (var row = 0; row < NUM_ROWS; row++)
        {
            sb.Append("|");

            for (var col = 0; col < NUM_COLS; col++)
            {
                var number = Numbers[row, col];

                sb.Append(number.ToString().PadLeft(2));
                sb.Append(number == 0 ? "F" : " ");

                sb.Append("|");
            }

            sb.AppendLine();

            sb.AppendLine("+-----+-----+-----+-----+-----+");
        }

        return sb.ToString();
    }

    public bool IsWinner(int[,] stateCalledBalls)
    {
        throw new NotImplementedException();
    }

    public bool ContainsNumber(int currentNumber)
    {
        throw new NotImplementedException();
    }

    public bool IsWinner(List<int> stateCalledBalls)
    {
        throw new NotImplementedException();
    }
}