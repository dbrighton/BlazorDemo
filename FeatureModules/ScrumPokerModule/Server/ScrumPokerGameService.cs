using Microsoft.Extensions.Hosting;

namespace ScrumPokerFeatureModule.Server;



public class ScrumPokerGameService 
{
    private readonly ILogger<ScrumPokerGameService> _logger;
    private readonly EventAggregator _ea;
    private readonly ScrumPokerHub _hub;


    public List<ScrumPokerSession> Sessions { get; set; } = new List<ScrumPokerSession>();



    public ScrumPokerGameService(ILogger<ScrumPokerGameService> logger, ScrumPokerHub hub, EventAggregator ea)
    {
        _logger = logger;
        _ea = ea;
        _hub = hub;

        _ea.GetEvent<ScrumPokerFeatureAddedEvent>().Subscribe((payload) =>
        {
            if (this.Sessions.Any(x => x.Id == payload.Id))
            {
                throw new Exception("Story Name already exists");
            }

            this.Sessions.Add(payload);
            _hub.Clients.All.SendAsync(Constants.PokerSessionsUpdated, this.Sessions);
        });
    }

   

   
}

