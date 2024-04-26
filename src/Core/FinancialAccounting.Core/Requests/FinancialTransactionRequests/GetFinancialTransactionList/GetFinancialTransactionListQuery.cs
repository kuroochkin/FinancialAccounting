using FinancialAccounting.Contracts.Requests.FinancialTransactionRequests.GetFinancialTransactionList;
using FinancialAccounting.Core.Models;
using MediatR;

namespace FinancialAccounting.Core.Requests.FinancialTransactionRequests.GetFinancialTransactionList;

/// <summary>
/// Запрос списка финансовых транзакций
/// </summary>
public class GetFinancialTransactionListQuery : IRequest<GetFinancialTransactionListResponse>
{
    /// <summary>
    /// Фильтр
    /// </summary>
    public FinancialTransactionFilter? Filter { get; set; }
}