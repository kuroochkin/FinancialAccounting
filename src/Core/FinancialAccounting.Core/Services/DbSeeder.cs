using FinancialAccounting.Core.Abstractions;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinancialAccounting.Core.Services;

/// <summary>
/// Сервис добавления данных в БД
/// </summary>
public class DbSeeder : IDbSeeder
{
    /// <inheritdoc/>
    public async Task SeedAsync(
        IDbContext dbContext, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        
        await SeedUsersAsync(dbContext, cancellationToken);
        await SeedBankAccountsAsync(dbContext, cancellationToken);
        await SeedCategoriesAsync(dbContext, cancellationToken);
        await SeedFinancialTransactionsAsync(dbContext, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task SeedUsersAsync(
        IDbContext dbContext, 
        CancellationToken cancellationToken)
    {
        var isExist = await dbContext.Users.AnyAsync(cancellationToken);
        
        var passwordHashService = new PasswordEncryptionService();
        var passwordHash = passwordHashService.EncodePassword("123ABC!");
        
        if (!isExist)
        {
            var user = new User(
                login: "testuser1",
                passwordHash: passwordHash,
                email: "uoldiz111@gmail.com",
                phone: "89435712332");

            await dbContext.Users.AddRangeAsync(user);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    
    private async Task SeedBankAccountsAsync(
        IDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var isExist = await dbContext.BankAccounts.AnyAsync(cancellationToken);

        if (!isExist)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(cancellationToken);

            var bankAccount1 = new BankAccount(
                balance: 54000,
                title: "Тестовый счет",
                user: user
            );
            
            var bankAccount2 = new BankAccount(
                balance: 36000,
                title: "Запаска",
                user: user
            );
            
            await dbContext.BankAccounts.AddRangeAsync(bankAccount1, bankAccount2);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    
    private async Task SeedCategoriesAsync(
        IDbContext dbContext, 
        CancellationToken cancellationToken)
    {
        var isExist = await dbContext.Categories.AnyAsync(cancellationToken);

        if (!isExist)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(cancellationToken);

            var category1 = new Category(
                userId: user!.Id,
                title: "Продукты"
            );
            
            var category2 = new Category(
                userId: user!.Id,
                title: "Досуг"
            );
            
            var category3 = new Category(
                userId: user!.Id,
                title: "Подарки"
            );
            
            dbContext.Categories.AddRange(category1, category2, category3);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    
    private async Task SeedFinancialTransactionsAsync(
        IDbContext dbContext, 
        CancellationToken cancellationToken)
    {
        var isExist = await dbContext.FinancialTransactions.AnyAsync(cancellationToken);

        if (!isExist)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(cancellationToken);
            var bankAccounts = dbContext.BankAccounts.ToArray();
            var categories = dbContext.Categories.ToArray();

            var financialTransaction1 = new FinancialTransaction(
                amount: 120,
                type: TransactionType.Consumption,
                category: categories[0],
                bankAccount: bankAccounts[0],
                actualDate: DateTime.Now,
                userId: user!.Id
            );
            
            var financialTransaction2 = new FinancialTransaction(
                amount: 250,
                type: TransactionType.Consumption,
                category: categories[1],
                bankAccount: bankAccounts[0],
                actualDate: DateTime.Now,
                userId: user!.Id
            );
            
            var financialTransaction3 = new FinancialTransaction(
                amount: 290,
                type: TransactionType.Income,
                category: categories[0],
                bankAccount: bankAccounts[0],
                actualDate: DateTime.Now,
                userId: user!.Id
            );
            
            var financialTransaction4 = new FinancialTransaction(
                amount: 354,
                type: TransactionType.Consumption,
                category: categories[2],
                bankAccount: bankAccounts[1],
                actualDate: DateTime.Now,
                userId: user!.Id
            );
            
            dbContext.FinancialTransactions.AddRange(financialTransaction1, financialTransaction2,
                financialTransaction3, financialTransaction4);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}