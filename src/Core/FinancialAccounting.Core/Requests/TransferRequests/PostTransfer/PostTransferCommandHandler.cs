using FinancialAccounting.Contracts.Requests.TransferRequests.PostTransfer;
using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Requests.TransferRequests.PostTransfer;

/// <summary>
/// Обработчик запроса <see cref="PostTransferCommand"/>
/// </summary>
public class PostTransferCommandHandler
    : IRequestHandler<PostTransferCommand, PostTransferResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public PostTransferCommandHandler(
        IDbContext dbContext,
        IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }
    
    /// <inheritdoc/>
    public async Task<PostTransferResponse> Handle(
        PostTransferCommand request, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        if (request.Amount == default)
            throw new RequiredFieldNotSpecifiedException();
        
        var bankAccounts = await _dbContext.BankAccounts
            .Where(x => x.Id == request.FromBankAccountId || x.Id == request.ToBankAccountId)
            .ToListAsync(cancellationToken);

        var fromBankAccount = bankAccounts.SingleOrDefault(x => x.Id == request.FromBankAccountId)
            ?? throw new NotFoundException($"Банковский счет с Id: {request.FromBankAccountId} не найден");

        var toBankAccount = bankAccounts.SingleOrDefault(x => x.Id == request.ToBankAccountId)
            ?? throw new NotFoundException($"Банковский счет с Id: {request.ToBankAccountId} не найден");

        if (fromBankAccount.UserId != _userContext.CurrentUserId || toBankAccount.UserId != _userContext.CurrentUserId)
            throw new ApplicationExceptionBase(
                "Банковские счета должны принадлежать пользователю, который совершает перевод");

        var transfer = new Transfer(
            amount: request.Amount,
            comment: request.Comment,
            fromBankAccount: fromBankAccount,
            toBankAccount: toBankAccount);
        
        foreach (var bankAccount in bankAccounts)
            bankAccount.AddTransfer(transfer);

        _dbContext.Transfers.Add(transfer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostTransferResponse
        {
            Id = transfer.Id
        };
    }
}