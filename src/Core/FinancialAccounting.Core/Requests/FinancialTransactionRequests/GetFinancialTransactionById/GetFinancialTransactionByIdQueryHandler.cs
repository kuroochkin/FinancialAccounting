using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionById;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionById;

/// <summary>
/// Обработчик запроса <see cref="GetFinancialTransactionByIdQuery"/>
/// </summary>
public class GetFinancialTransactionByIdQueryHandler
    : IRequestHandler<GetFinancialTransactionByIdQuery, GetFinancialTransactionByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public GetFinancialTransactionByIdQueryHandler (
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }
    
    /// <inheritdoc/>
    public async Task<GetFinancialTransactionByIdResponse> Handle(
        GetFinancialTransactionByIdQuery request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var financialTransaction = await _dbContext.FinancialTransactions
            .Include(a => a.Category)
            .Include(a => a.BankAccount)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Финансовая транзакция с Id: {request.Id} не найдена");
        
        if (financialTransaction.UserId != _userContext.CurrentUserId)
            throw new ApplicationExceptionBase(
                "Просматривать фин.транзакцию может только тот пользователь, который её создал");

        return new GetFinancialTransactionByIdResponse
        {
            Id = financialTransaction.Id,
            UserId = financialTransaction.UserId,
            Amount = financialTransaction.Amount,
            Type = financialTransaction.Type,
            Comment = financialTransaction.Comment,
            ActualDate = financialTransaction.ActualDate,
            CreatedOn = financialTransaction.CreatedOn,
            BankAccount = financialTransaction.BankAccount != null
                ? new GetFinancialTransactionByIdResponseBankAccount
                {
                    Id = financialTransaction.BankAccount.Id,
                    Title = financialTransaction.BankAccount?.Title,
                    Balance = financialTransaction.BankAccount!.Balance
                }
                : default,
            Category = financialTransaction.Category != null
                ? new GetFinancialTransactionByIdCategory
                {
                    Id = financialTransaction.Category.Id,
                    Title = financialTransaction.Category.Title
                }
                : default
        };
    }
}