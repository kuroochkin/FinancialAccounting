using FinancialAccounting.Domain.Exceptions;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Сущность фото, подтверждающего операцию
/// </summary>
public class Photo : EntityBase
{
    /// <summary>
    /// Поле для <see cref="_file"/>
    /// </summary>
    public const string FileField = nameof(_file);
    
    /// <summary>
    /// Поле для <see cref="_financialTransaction"/>
    /// </summary>
    public const string FinancialTransactionField = nameof(_financialTransaction);
    
    private File? _file;
    private FinancialTransaction? _financialTransaction;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="file">Файл</param>
    /// <param name="financialTransaction">Операция</param>
    public Photo(
        File? file,
        FinancialTransaction financialTransaction)
    {
        File = file;
        FinancialTransaction = financialTransaction;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected Photo()
    {
    }
    
    /// <summary>
    /// Финансовая операция
    /// </summary>
    public Guid FinancialTransactionId { get; private set; }
    
    /// <summary>
    /// Файл
    /// </summary>
    public Guid FileId { get; private set; }

    /// <summary>
    /// Финансовая операция
    /// </summary>
    public FinancialTransaction? FinancialTransaction
    {
        get => _financialTransaction;
        set
        {
            _financialTransaction = value
                ?? throw new RequiredFieldNotSpecifiedException("Операция");
            FinancialTransactionId = value.Id;
        }
    }

    /// <summary>
    /// Файл
    /// </summary>
    public File? File
    {
        get => _file;
        set
        {
            _file = value
                ?? throw new RequiredFieldNotSpecifiedException("Файл");
            FileId = value.Id;
        }
    }
}