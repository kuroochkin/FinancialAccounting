namespace FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;

/// <summary>
/// Запрос на регистрацию студента
/// </summary>
public class RegisterStudentRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string Phone { get; set; } = default!;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birthday { get; set; }
}