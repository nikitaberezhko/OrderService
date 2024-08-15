namespace Services.Models.OtherModels;

public class DownTimeModel
{
    public Guid Id { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public Guid HubId { get; set; }
}