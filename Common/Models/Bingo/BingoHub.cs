namespace Shell.API.Games.Bingo
{
    /// <summary>
    /// SignalR hub for managing Bingo games.
    /// </summary>
    public class BingoHub : Hub
    {
        private readonly ILogger<BingoHub> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of the <see cref="BingoHub"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{BingoHub}"/> used for logging.</param>
        /// <param name="mediator">An instance of <see cref="IMediator"/> used for sending and publishing MediatR messages.</param>
        public BingoHub(ILogger<BingoHub> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Adds the current connection to the "bingoPlayers" group and broadcasts a "PlayerJoined" message to other group members.
        /// </summary>
        /// <param name="playerName">The name of the player to join.</param>
        public async Task JoinPlayer(string playerName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "bingoPlayers");
            await Clients.OthersInGroup("bingoPlayers").SendAsync("PlayerJoined", playerName);
        }

        /// <summary>
        /// Removes the current connection from the "bingoPlayers" group and broadcasts a "PlayerLeft" message to other group members.
        /// </summary>
        /// <param name="playerName">The name of the player to leave.</param>
        public async Task LeavePlayer(string playerName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "bingoPlayers");
            await Clients.OthersInGroup("bingoPlayers").SendAsync("PlayerLeft", playerName);
        }

        /// <summary>
        /// Starts a new Bingo game by publishing a <see cref="StartNewBingoGameAction"/> event via MediatR, and broadcasting a "GameStarted" message to all clients.
        /// </summary>
        public async Task StartNewGame()
        {
            try
            {
                await _mediator.Publish(new StartNewBingoGameAction());

                await Clients.All.SendAsync("GameStarted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting the game");
                throw;
            }
        }
    }
}