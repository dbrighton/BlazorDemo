namespace Common.Models.Bingo
{
    /// <summary>
    /// Represents the state of a Bingo game.
    /// </summary>
    public class BingoGameState
    {
        /// <summary>
        /// The current list of drawn numbers.
        /// </summary>
        public List<int> DrawnNumbers { get; set; } = new List<int>();

        /// <summary>
        /// The current list of Bingo cards in play.
        /// </summary>
        public List<BingoCard> Cards { get; set; } = new List<BingoCard>();

        /// <summary>
        /// The current number being drawn.
        /// </summary>
        public int CurrentNumber { get; set; }

        /// <summary>
        /// The next number to be drawn.
        /// </summary>
        public int NextNumber { get; set; }

        /// <summary>
        /// The list of players in the Bingo game.
        /// </summary>
        public List<BingoPlayer> Players { get; set; }
    }
}