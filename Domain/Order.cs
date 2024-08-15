namespace Domain;

/// <summary>
/// Заказ
/// </summary>
public class Order
{
    public Guid Id { get; set; }
    
    public Guid ClientId { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public Guid HubStartId { get; set; }
    
    public Guid HubEndId { get; set; }
    
    public double Price { get; set; }
    
    public double Costs { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public virtual ICollection<Container> Containers { get; set; }
    
    public virtual ICollection<DownTime> DownTimes { get; set; }
}