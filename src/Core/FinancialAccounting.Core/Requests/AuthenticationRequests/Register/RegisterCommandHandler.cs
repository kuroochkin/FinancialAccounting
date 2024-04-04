using FinancialAccounting.Contracts.Requests.AuthenticationRequests.Register;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Enums;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.AuthenticationRequests.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IClaimsIdentityFactory _claimsIdentityFactory;
    private readonly IPasswordEncryptionService _passwordEncryptionService;
    private readonly ITokenAuthenticationService _tokenAuthenticationService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="claimsIdentityFactory">Фабрика ClaimsPrincipal для пользователей</param>
    /// <param name="passwordEncryptionService">Сервис хэширования паролей</param>
    /// <param name="tokenAuthenticationService">Сервис работы с токенами</param>
    public RegisterCommandHandler(
        IDbContext dbContext,
        IClaimsIdentityFactory claimsIdentityFactory,
        IPasswordEncryptionService passwordEncryptionService,
        ITokenAuthenticationService tokenAuthenticationService)
    {
        _dbContext = dbContext;
        _claimsIdentityFactory = claimsIdentityFactory;
        _passwordEncryptionService = passwordEncryptionService;
        _tokenAuthenticationService = tokenAuthenticationService;
    }
    
    public async Task<RegisterResponse> Handle(
        RegisterCommand request, 
        CancellationToken cancellationToken)
    {
        
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrEmpty(request.Phone)
            || string.IsNullOrEmpty(request.Email)
            || string.IsNullOrEmpty(request.Password))
            throw new RequiredFieldNotSpecifiedException();
        
        var isExist = await _dbContext.Users.AnyAsync(
            x => x.Login == request.Login
                 || x.Phone == request.Phone
                 || x.Email == request.Email, cancellationToken);

        if (isExist)
            throw new ApplicationExceptionBase("Укажите уникальный логин, e-mail и номер телефона");
        
        var passwordHash = _passwordEncryptionService.EncodePassword(request.Password);

        var user = new User(
            login: request.Login,
            passwordHash: passwordHash,
            email: request.Email,
            phone: request.Phone);
        
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var claims = _claimsIdentityFactory.CreateClaimsIdentity(user);
        var token = _tokenAuthenticationService.CreateToken(claims, TokenTypes.Auth);
        
        return new RegisterResponse(userId: user.Id, token: token);
    }
}