namespace FluxorChess.API
{
    public  class ChessHub : Hub
    {
        private readonly ILogger<ChessHub> _log;

        public ChessHub(ILogger<ChessHub> log)
        {
            _log = log;
        }
    }
}
