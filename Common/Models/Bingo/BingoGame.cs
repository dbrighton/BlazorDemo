using Common.Models.Bingo;

namespace Shell.API.Games.Bingo
{
    /// <summary>
    /// Represents a Bingo game.
    /// </summary>
    public class BingoGame
    {
        private readonly ILogger<BingoGame> _logger;
        private readonly BingoHub _bingoHub;
        private readonly IMediator _mediator;

        private readonly List<BingoPlayer> _players;
        private readonly List<BingoCard> _cards;
        private readonly Cage _cage;
        private int _nextNumber;
        private int _currentNumber;
        private bool _isRunning;

        /// <summary>
        /// Creates a new instance of the <see cref="BingoGame"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{BingoGame}"/> used for logging.</param>
        /// <param name="bingoHub">An instance of <see cref="BingoHub"/> used for broadcasting Bingo events to connected clients.</param>
        /// <param name="mediator">An instance of <see cref="IMediator"/> used for sending and publishing MediatR messages.</param>
        public BingoGame(ILogger<BingoGame> logger, BingoHub bingoHub, IMediator mediator)
        {
            _logger = logger;
            _bingoHub = bingoHub;
            _mediator = mediator;

            _players = new List<BingoPlayer>();
            _cards = new List<BingoCard>();
            _cage = new Cage();
            _nextNumber = 1;
            
            _logger.LogInformation("Bingo game created.");
        }

        /// <summary>
        /// Gets the current state of the Bingo game.
        /// </summary>
        public BingoGameState GetState()
        {
            _logger.LogInformation($"Getting state of Bingo game.");
            return new BingoGameState
            {
                Players = _players,
                Cards = _cards,
                NextNumber = _nextNumber
            };
        }

        /// <summary>
        /// Adds a player to the Bingo game.
        /// </summary>
        /// <param name="player">The player to add.</param>
        public void AddPlayer(BingoPlayer player)
        {
            _logger.LogInformation($"Adding player {player.Name} to Bingo game.");
            _players.Add(player);
        }

        /// <summary>
        /// Removes a player from the Bingo game.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        public void RemovePlayer(BingoPlayer player)
        {
            _logger.LogInformation($"Removing player {player.Name} from Bingo game.)");
            _players.Remove(player);
        }

        /// <summary>
        /// Adds a card to the Bingo game.
        /// </summary>
        /// <param name="card">The card to add.</param>
        public void AddCard(BingoCard card)
        {
            _logger.LogInformation("Adding card to Bingo game.");
            _cards.Add(card);
        }

        /// <summary>
        /// Removes a card from the Bingo game.
        /// </summary>
        /// <param name="card">The card to remove.</param>
        public void RemoveCard(BingoCard card)
        {
            _logger.LogInformation($"removing card {card.Id} from Bingo game.");
            _cards.Remove(card);
        }

        /// <summary>
        /// Starts the Bingo game by continuously drawing numbers from the Cage and checking each player's Bingo card for matches.
        /// </summary>
        /// <param name="delayMilliseconds">The delay in milliseconds between each number draw.</param>
        public async Task Start(int delayMilliseconds)
        {
            _logger.LogInformation($"starting Bingo game.");
            // Initialize the game state
            _currentNumber = _cage.DrawNumber();
            _isRunning = true;

            // Notify the players that the game has started
            await _bingoHub.Clients.Group("bingoPlayers").SendAsync("GameStarted");

            // Loop until the game is over or stopped
            while (_isRunning)
            {
                // Wait for the specified delay time
                await Task.Delay(delayMilliseconds);

                // Draw a new number from the Cage
                var newNumber = _cage.DrawNumber();

                // Update the game state with the new number
                _currentNumber = newNumber;

                // Check each player's Bingo card for matches
                foreach (var player in _players)
                {
                    if (player.BingoCard.ContainsNumber(_currentNumber))
                    {
                        player.Score += 1;

                        // Notify the player that they have a match
                        await _bingoHub.Clients.Client(player.ConnectionId).SendAsync("MatchMade", _currentNumber);
                    }
                }
            }
        }
    }
}