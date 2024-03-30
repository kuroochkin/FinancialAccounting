using FinancialAccounting.Core.Abstractions;

namespace FinancialAccounting.Core.Services;

/// <summary>
/// Провайдер даты
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}