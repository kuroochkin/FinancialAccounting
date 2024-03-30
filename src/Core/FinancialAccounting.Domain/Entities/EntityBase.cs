namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// ИД сущности
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата создания сущности
    /// </summary>
    public DateTime CreatedOn { get; set; }
    
    /// <summary>
    /// Дата последнего изменения сущности
    /// </summary>
    public DateTime ModifiedOn { get; set; }
}