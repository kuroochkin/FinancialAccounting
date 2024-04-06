using FinancialAccounting.Contracts.Requests.TransferRequests.GetTransferById;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.TransferRequests.GetTransferById;

/// <summary>
/// Обработчик запроса <see cref="GetTransferByIdQuery"/>
/// </summary>
public class GetTransferByIdQueryHandler 
    : IRequestHandler<GetTransferByIdQuery, GetTransferByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public GetTransferByIdQueryHandler (
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }
    
    /// <inheritdoc/>
    public async Task<GetTransferByIdResponse> Handle(
        GetTransferByIdQuery request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var transfer = await _dbContext.Transfers
            .Include(x => x.FromBankAccount)
            .Include(x => x.ToBankAccount)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Перевод с Id: {request.Id} не найден");

        if (transfer.FromBankAccount != null && transfer.FromBankAccount.UserId != _userContext.CurrentUserId)
            throw new ApplicationExceptionBase("Просматривать перевод может только тот пользователь, который его совершил");
            
        return new GetTransferByIdResponse
        {
            Id = transfer.Id,
            Comment = transfer.Comment,
            Amount = transfer.Amount,
            CreatedDate = transfer.CreatedOn,
            FromBankAccount = transfer.FromBankAccount != null
                ? new BankAccountResponse
                {
                    Id = transfer.FromBankAccountId,
                    Title = transfer.FromBankAccount.Title
                }
                : default,
            ToBankAccount = transfer.ToBankAccount != null
                ? new BankAccountResponse
                {
                    Id = transfer.ToBankAccountId,
                    Title = transfer.ToBankAccount.Title
                }
                : default
        };
    }
}