using FinancialAccounting.Contracts.Requests.BankAccountRequests.PostBankAccount;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.BankAccountRequests.PostBankAccount;

/// <summary>
/// Обработчик запроса <see cref="PostBankAccountCommand"/>
/// </summary>
public class PostBankAccountCommandHandler
    : IRequestHandler<PostBankAccountCommand, PostBankAccountResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public PostBankAccountCommandHandler(
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }
    
    /// <inheritdoc/>
    public async Task<PostBankAccountResponse> Handle(
        PostBankAccountCommand request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        if (request.Balance == default)
            throw new RequiredFieldNotSpecifiedException("Сумма счета");
        
        if (string.IsNullOrEmpty(request.Title))
            throw new RequiredFieldNotSpecifiedException("Название счета");

        var currentUser = await _dbContext.Users
              .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken: cancellationToken) 
                ?? throw new NotFoundException($"Пользователь с Id {_userContext.CurrentUserId} не найден");
        
        var bankAccount = new BankAccount(
            balance: request.Balance,
            title: request.Title,
            user: currentUser);

        _dbContext.BankAccounts.Add(bankAccount);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostBankAccountResponse
        {
            Id = bankAccount.Id
        };
    }
}