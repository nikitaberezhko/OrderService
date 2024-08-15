namespace Domain;

/// <summary>
/// Время простоя
/// </summary>
public class DownTime
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    /// <summary>
    /// Дата начала простоя
    /// </summary>
    public DateTime DateStart { get; set; }
    
    /// <summary>
    /// Дата конца простоя
    /// </summary>
    public DateTime DateEnd { get; set; }
    
    /// <summary>
    /// Id хаба
    /// </summary>
    public Guid HubId { get; set; }
    
    public bool IsDeleted { get; set; }
}