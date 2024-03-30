using System.ComponentModel.DataAnnotations;

namespace FinancialAccounting.Domain.Entities;

/// <summary>
/// Сущность файла
/// </summary>
public class File : EntityBase
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="address">Адрес в S3</param>
    /// <param name="name">Название файла</param>
    /// <param name="size">Размер файла в байтах</param>
    /// <param name="photo">Объект фотографии</param>
    /// <param name="mimeType">Тип файла</param>
    public File(
        string address,
        string name,
        long size,
        Photo photo,
        string? mimeType = null)
    {
        
        if (string.IsNullOrWhiteSpace(address))
            throw new ValidationException("Не задан адрес файла в S3-хранилище");

        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Не задано название файла");

        if (size <= 0)
            throw new ValidationException($"Некорректный размер файла в байтах: {size}");
        
        ArgumentNullException.ThrowIfNull(photo);

        Address = address;
        FileName = name;
        Size = size;
        Photo = photo;
        ContentType = mimeType;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected File()
    {
    }

    /// <summary>
    /// Идентификатор для S3-хранилища
    /// </summary>
    public string Address { get; private set; } = default!;

    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Размер файла в байтах
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// Mime-тип
    /// </summary>
    public string? ContentType { get; private set; }

    /// <summary>
    /// Идентификатор фото
    /// </summary>
    public Guid PhotoId { get; private set; }
    
    /// <summary>
    /// Фото, к которому привязан файл
    /// </summary>
    public Photo Photo { get; private set; }
}