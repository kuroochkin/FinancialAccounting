using System.Security.Cryptography;
using System.Text;
using FinancialAccounting.Core.Abstractions;

namespace FinancialAccounting.Core.Services;

/// <summary>
/// Сервис хэширования паролей.
/// </summary>
public class PasswordEncryptionService : IPasswordEncryptionService
{
    private readonly string _passwordHashSalt = "12345abcd";

    /// <summary>
    /// Хэшировать пароль.
    /// </summary>
    /// <param name="password">Пароль в чистом виде.</param>
    /// <returns>Хэш пароля.</returns>
    public string EncodePassword(string password)
        => CreateHash(password, _passwordHashSalt);

    /// <summary>
    /// Проверить хэш пароля на корректность.
    /// </summary>
    /// <param name="password">Пароль в чистом виде.</param>
    /// <param name="encodedPassword">Хэш пароля.</param>
    /// <returns>TRUE, если хэш пароля в чистом виде совпадает с хэшем пароля.</returns>
    public bool ValidatePassword(string password, string encodedPassword)
        => encodedPassword == CreateHash(password, _passwordHashSalt);

    /// <summary>
    /// Создать хэш пароля.
    /// </summary>
    /// <param name="password">Пароль.</param>
    /// <param name="salt">Соль для хэширования.</param>
    /// <returns>Хэш пароля.</returns>
    private static string CreateHash(string password, string salt)
    {
        password ??= string.Empty;
        salt ??= string.Empty;

        var saltedValue = Encoding.UTF8.GetBytes($"{password}{salt}");
        return Convert.ToBase64String(SHA512.HashData(saltedValue));
    }
}