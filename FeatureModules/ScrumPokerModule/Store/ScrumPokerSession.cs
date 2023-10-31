using System.ComponentModel.DataAnnotations;

namespace ScrumPokerFeatureModule.Store;

public class ScrumPokerSession
{
    public ScrumPokerSession(Person scrumMaster, string story, string storyDetails)
    {
        if (string.IsNullOrWhiteSpace(story) || string.IsNullOrWhiteSpace(storyDetails))
            throw new Exception("Story and StoryDetails are required");


        var players = new List<PokerPlayer>();


        ScrumMaster = scrumMaster;
        Story = story;
        StoryDetails = storyDetails;
        Players = players;
        ScrumMasterName = scrumMaster.email;
    }

    public ScrumPokerSession(Person scrumMaster, string story, string storyDetails, List<PokerPlayer>? players)
    {
        if (string.IsNullOrWhiteSpace(story) || string.IsNullOrWhiteSpace(storyDetails))
            throw new Exception("Story and StoryDetails are required");

        if (players == null) players = new List<PokerPlayer>();

        ScrumMaster = scrumMaster;
        Story = story;
        StoryDetails = storyDetails;
        Players = players;
        ScrumMasterName = scrumMaster.email;
    }

    [Display(Name = "Scrum Master")] public string ScrumMasterName { get; set; }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Person ScrumMaster { get; init; }
    public string Story { get; init; }
    public string StoryDetails { get; init; }
    public List<PokerPlayer> Players { get; init; }
}