namespace Common.Models.Bingo;

/// <summary>
/// Represents a cage containing Bingo balls.
/// </summary>
public class Cage
{
    private readonly List<int> _balls;
    private readonly Random _random;

    /// <summary>
    /// Creates a new instance of the <see cref="Cage"/> class.
    /// </summary>
    public Cage()
    {
        _balls = Enumerable.Range(1, 75).ToList();
        _random = new Random();
    }

    /// <summary>
    /// Draws a ball from the cage and removes it from the list of available balls.
    /// </summary>
    /// <returns>The value of the drawn ball.</returns>
    public int DrawBall()
    {
        if (_balls.Count == 0)
        {
            throw new InvalidOperationException("No more balls in the cage.");
        }

        var index = _random.Next(_balls.Count);
        var ball = _balls[index];
        _balls.RemoveAt(index);

        return ball;
    }

    /// <summary>
    /// Resets the cage to its initial state with all 75 balls.
    /// </summary>
    public void Reset()
    {
        _balls.Clear();
        _balls.AddRange(Enumerable.Range(1, 75));
        Shuffle();
    }

    /// <summary>
    /// Draws a random number from the cage and remove it from the list of available numbers.
    /// </summary>
    /// <returns>The value of the drawn number.</returns>
    public int DrawNumber()
    {
        if (_balls.Count == 0)
        {
            Reset();
        }

        var index = _random.Next(_balls.Count);
        var ball = _balls[index];
        _balls.RemoveAt(index);

        return ball;
    }

    /// <summary>
    /// Shuffles the order of the balls in the cage.
    /// </summary>
    public void Shuffle()
    {
        for (var i = 0; i < _balls.Count; i++)
        {
            var j = _random.Next(i, _balls.Count);
            var temp = _balls[i];
            _balls[i] = _balls[j];
            _balls[j] = temp;
        }
    }
}