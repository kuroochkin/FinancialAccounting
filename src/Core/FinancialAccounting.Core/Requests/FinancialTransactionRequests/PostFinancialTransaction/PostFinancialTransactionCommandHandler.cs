using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.PostFinancialTransaction;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.PostFinancialTransaction;

/// <summary>
/// Обработчик для команды <see cref="PostFinancialTransactionCommand"/>
/// </summary>
public class PostFinancialTransactionCommandHandler
    : IRequestHandler<PostFinancialTransactionCommand, PostFinancialTransactionResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public PostFinancialTransactionCommandHandler(
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }
    
    /// <inheritdoc/>
    public async Task<PostFinancialTransactionResponse> Handle(
        PostFinancialTransactionCommand request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var userId = _userContext.CurrentUserId;

        var category = await _dbContext.Categories
            .Include(a => a.FinancialTransactions)
            .FirstOrDefaultAsync(a => a.Id == request.CategoryId, cancellationToken)
            ?? throw new NotFoundException($"Категория с Id {request.CategoryId} не найдена");

        var bankAccount = await _dbContext.BankAccounts
            .Include(a => a.FinancialTransactions)
            .FirstOrDefaultAsync(a => a.Id == request.BankAccountId, cancellationToken)
            ?? throw new NotFoundException($"Банковский счет с Id {request.BankAccountId} не найден");
        
        category.CheckUserInDesiredCategory(userId);
        bankAccount.CheckUserInDesiredBankAccount(userId);
        
        var financialTransaction = new FinancialTransaction(
            amount: request.Amount,
            type: request.Type,
            comment: request.Comment,
            actualDate: request.ActualDate,
            category: category,
            bankAccount: bankAccount,
            userId: userId
        );

        bankAccount.AddOrUpdateFinancialTransaction(
            financialTransaction: financialTransaction,
            isNewTransaction: true);

        _dbContext.FinancialTransactions.Add(financialTransaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostFinancialTransactionResponse
        {
            Id = financialTransaction.Id,
            CurrentBalance = bankAccount.Balance
        };
    }
}