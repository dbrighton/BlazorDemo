namespace FluxorChess.Models;

public class GameInfo
{
    public Guid? GameId { get; set; }

    [Display(Name = "Create By")] 
    public string CreateBy { get; set; } = string.Empty;


    [Display(Name = "Last Updated")]
    public DateTime LastUpdateTimeStamp { get; set; } = DateTime.UtcNow;

    public GameStatus GameStatus { get; set; }
    [JsonIgnore]
    public IClientProxy? HubCaller { get; set; }
}