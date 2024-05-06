using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PutFinancialTransaction;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.PutFinancialTransaction;

/// <summary>
/// Обработчик для команды <see cref="PutFinancialTransactionCommand"/>
/// </summary>
public class PutFinancialTransactionCommandHandler
    : IRequestHandler<PutFinancialTransactionCommand, PutFinancialTransactionResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public PutFinancialTransactionCommandHandler(
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }
    
    /// <inheritdoc/>
    public async Task<PutFinancialTransactionResponse> Handle(
        PutFinancialTransactionCommand request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var userId = _userContext.CurrentUserId;
        
        var financialTransaction = await _dbContext.FinancialTransactions
            .Include(a => a.BankAccount)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Финансовая транзакция с Id {request.Id} не найдена.");
        
        var bankAccount = await _dbContext.BankAccounts
            .Include(a => a.FinancialTransactions)
            .FirstOrDefaultAsync(a => a.Id == request.BankAccountId, cancellationToken)
            ?? throw new NotFoundException($"Банковский счет с Id {request.BankAccountId} не найден.");
        
        var category = await _dbContext.Categories
            .Include(a => a.FinancialTransactions)
            .FirstOrDefaultAsync(a => a.Id == request.CategoryId, cancellationToken)
            ?? throw new NotFoundException($"Категория с Id {request.CategoryId} не найдена");
        
        category.CheckUserInDesiredCategory(userId);
        bankAccount.CheckUserInDesiredBankAccount(userId);
        
        financialTransaction!.BankAccount?.AddOrUpdateFinancialTransaction(
            financialTransaction: financialTransaction,
            isNewTransaction: false);
        
        financialTransaction.Upsert(
            amount: request.Amount,
            actualDate: request.ActualDate,
            type: request.Type,
            comment: request.Comment,
            bankAccount: bankAccount,
            category: category);
        
        financialTransaction!.BankAccount?.AddOrUpdateFinancialTransaction(
            financialTransaction: financialTransaction,
            isNewTransaction: true);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PutFinancialTransactionResponse
        {
            Id = financialTransaction.Id,
            Amount = financialTransaction.Amount,
            Type = financialTransaction.Type
        };
    }
}