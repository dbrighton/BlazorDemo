namespace Common.Models
{
    /// <summary>
    /// Represents a base class for a game.
    /// </summary>
    public abstract class GameBase<T> where T : Hub
    {
        protected readonly IHubContext<T> _hubContext;
        protected List<Person> _players;

        protected GameBase(IHubContext<T> hubContext)
        {
            _hubContext = hubContext;
            _players = new List<Person>();
        }

        // Add other common game logic here
    }
}