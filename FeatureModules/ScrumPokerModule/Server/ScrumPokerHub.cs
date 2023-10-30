
namespace ScrumPokerFeatureModule.Server;

public class ScrumPokerHub :Hub
{
    private readonly ILogger<ScrumPokerHub> _logger;
    private readonly EventAggregator _ea;

    public ScrumPokerHub(ILogger<ScrumPokerHub> logger, EventAggregator ea)
    {
        _logger = logger;
        _ea = ea;
    }

    [HubMethodName(Constants.GetPokerSessions)]
    public async Task<List<ScrumPokerSession>> OnGetPokerSessions()
    {
        await Task.Delay(TimeSpan.FromSeconds(.001));
        _logger.LogInformation("GetPokerSessions");
        // var sessions = await _mediator.Send(new GetPokerSessionsQuery());
        var sessions = new List<ScrumPokerSession>();
        return sessions;
        //   await Clients.Caller.SendAsync(Constants.GetPokerSessions, sessions);
    }

    [HubMethodName(Constants.CreateSession)]
    public async Task OnCreateSession(Person scrumMaster, string storyName, string story)
    {
        await Task.Run(() =>
        {
            var session = new ScrumPokerSession(scrumMaster, storyName, story);
            _ea.GetEvent<ScrumPokerFeatureAddedEvent>().Publish(session);

        });
       
    }
}

