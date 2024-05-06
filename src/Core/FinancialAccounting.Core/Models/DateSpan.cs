namespace FinancialAccounting.Core.Models;

/// <summary>
/// Модель временного промежутка
/// </summary>
public class DateSpan
{
    /// <summary>
    /// Начало временного промежутка
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Конец временного промежутка
    /// </summary>
    public DateTime EndDate { get; set; }

    public bool IsValidDateSpan() => StartDate <= EndDate;
}