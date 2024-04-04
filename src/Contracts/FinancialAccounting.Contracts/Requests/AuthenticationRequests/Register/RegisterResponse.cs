namespace FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;

/// <summary>
/// Ответ на команду <see cref="RegisterStudentRequest"/>
/// </summary>
public class RegisterStudentResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="studentId">Id студента</param>
    /// <param name="token">Токен авторизации</param>
    public RegisterStudentResponse(
        Guid studentId,
        string token)
    {
        StudentId = studentId;
        Token = token;
    }

    /// <summary>
    /// Id студента
    /// </summary>
    public Guid StudentId { get; }

    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string Token { get; }
}