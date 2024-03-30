namespace FinancialAccounting.Domain.Abstractions;

/// <summary>
/// Сущность, удаляемая логически
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Признак удаленности
    /// </summary>
    bool IsDeleted { get; set; }
}