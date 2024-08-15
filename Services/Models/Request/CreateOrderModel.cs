using Services.Models.OtherModels;

namespace Services.Models.Request;

public class CreateOrderModel
{
    public Guid ClientId { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public Guid HubStartId { get; set; }
    
    public Guid HubEndId { get; set; }
    
    public double Price { get; set; }
    
    public ICollection<ContainerModel> Containers { get; set; }
    
    public ICollection<DownTimeModel> DownTimes { get; set; }
}