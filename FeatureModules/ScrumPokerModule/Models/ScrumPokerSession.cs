namespace ScrumPokerFeatureModule.Models;

public class ScrumPokerSession : IScrumPokerSession
{
    public Person ScrumMaster { get; internal set; }
    public List<Person> Players { get; internal set; }
    public string StoryName { get; internal set; }
    public string Story { get; internal set; }
    public DateTime LastUpdated { get; internal set; }
    public Guid Id { get; internal set; }

    public ScrumPokerSession(Person scrumMaster, string storyName, string story)
    {
        ScrumMaster = scrumMaster;
        StoryName = storyName;
        Story = story;
        Players = new List<Person>();
        LastUpdated = DateTime.UtcNow;
        Id= Guid.NewGuid();
    }

   

    public Task CreateSessionAsync(IScrumPokerSession session, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddPlayerAsync(Guid sessionId, PokerPlayer player, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemovePlayerAsync(Guid sessionId, PokerPlayer player, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStoryAsync(Guid sessionId, string story, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task PlayAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EndGameAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}