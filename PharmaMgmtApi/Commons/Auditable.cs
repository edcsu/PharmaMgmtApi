namespace PharmaMgmtApi.Commons;

public abstract class Auditable : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}