namespace Services.Models.Request;

public class GetOrdersInPeriodModel
{
    public DateTime End { get; set; }
    
    public int Period { get; set; }
}