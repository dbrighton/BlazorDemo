// Ignore Spelling: Fluxor API

namespace FluxorChess.API
{
    /// <summary>
    /// Represents a hub for managing chess games.
    /// </summary>
    public class ChessHub : Hub
    {
        private readonly ILogger<ChessHub> _log;
        private readonly IEventAggregator _ea;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessHub"/> class.
        /// </summary>
        /// <param name="log">The logger.</param>
        /// <param name="ea">The event aggregator.</param>
        public ChessHub(ILogger<ChessHub> log, IEventAggregator ea)
        {
            _log = log;
            _ea = ea;
        }


        /// <summary>
        /// Joins an existing chess game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HubMethodName(HubConstants.JoinGame)]
        public Task JoinGame(ChessGame game)
        {
            game.HubClients = Clients;
            _ea.GetEvent<JoinGamePrismEvent>().Publish(game);
            return Task.CompletedTask;
        }
    }
}