using System.ComponentModel.DataAnnotations;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Сущность категории 
/// </summary>
public class Category : EntityBase
{
    /// <summary>
    /// Поле для <see cref="_financialTransactions"/>
    /// </summary>
    public const string FinancialTransactionsField = nameof(_financialTransactions);
    
    private List<FinancialTransaction>? _financialTransactions;
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid? UserId { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="title">Название категории</param>
    public Category(string title)
    {
        Title = title;
        _financialTransactions = new List<FinancialTransaction>();
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected Category()
    {
    }
    
    /// <summary>
    /// Список операций для конкретной категории
    /// </summary>
    public IReadOnlyList<FinancialTransaction>? FinancialTransactions => _financialTransactions;

    /// <summary>
    /// Редактирование названия категории
    /// </summary>
    /// <param name="title">Название категории</param>
    public void EditCategoryTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ValidationException("Не задано новое название для категории");

        Title = title;
    }
}