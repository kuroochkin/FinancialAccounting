using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionList;

/// <summary>
/// Обработчик запроса <see cref="GetFinancialTransactionListQuery"/>
/// </summary>
public class GetFinancialTransactionListQueryHandler
    : IRequestHandler<GetFinancialTransactionListQuery, GetFinancialTransactionListResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public GetFinancialTransactionListQueryHandler (
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    public async Task<GetFinancialTransactionListResponse> Handle(
        GetFinancialTransactionListQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var filter = request.Filter;

        var query = _dbContext.FinancialTransactions
            .Include(a => a.BankAccount)
            .Include(a => a.Category)
            .Where(a => a.UserId == _userContext.CurrentUserId);

        var filterQuery = query;
        
        if (filter!.CategoryId != null)
        {
            _ = query.Any(a => a.CategoryId == filter.CategoryId)
                ? filterQuery = filterQuery.Where(a => a.CategoryId == filter.CategoryId)
                : throw new NotFoundException($"Не удалось найти категорию с Id {filter?.CategoryId}.");
        }

        if (filter!.BankAccountId != null)
        {
            _ = query.Any(a => a.BankAccountId == filter.BankAccountId)
                ? filterQuery = filterQuery.Where(a => a.BankAccountId == filter.BankAccountId)
                : throw new NotFoundException($"Не удалось найти банковский счет с Id {filter?.BankAccountId}.");
        }
        
        if (filter.DateSpan != null)
        {
            _ = filter.DateSpan.IsValidDateSpan()
                ? filterQuery = filterQuery.Where(a => a.ActualDate >= filter.DateSpan.StartDate &&
                                                    a.ActualDate <= filter.DateSpan.EndDate)
                : throw new ApplicationException("Начальная дата не может превосходить конечную.");
        }
        
        filterQuery = filterQuery.Where(a => a.Type == filter.TransactionType);

        var financialTransactions = await filterQuery
            .Select(a => new GetFinancialTransactionListResponseItem
            {
                Id = a.Id,
                Amount = a.Amount,
                Type = a.Type,
                Comment = a.Comment,
                CreatedOn = a.CreatedOn,
                ActualDate = a.ActualDate,
                BankAccount = a.BankAccount != null
                    ? new GetFinancialTransactionListResponseItemBankAccount
                    {
                        Id = a.BankAccount.Id,
                        Title = a.BankAccount.Title
                    }
                    : null,
                Category = a.Category != null
                    ? new GetFinancialTransactionListResponseItemCategory
                    {
                        Id = a.Category.Id,
                        Title = a.Category.Title
                    }
                    : null
            })
            .OrderByDescending(x => x.ActualDate)
            .ToListAsync(cancellationToken);

        return new GetFinancialTransactionListResponse(
            userId: _userContext.CurrentUserId,
            financialTransactions: financialTransactions);
    }
}