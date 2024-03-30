using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="BankAccount"/>
/// </summary>
internal class BankAccountConfiguration : EntityBaseConfiguration<BankAccount>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("bank_account", "public")
            .HasComment("Банковский счет");

        builder.Property(p => p.Balance)
            .HasComment("Баланс")
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasComment("Идентификатор пользователя")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(y => y.BankAccounts)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.FinancialTransactions)
            .WithOne(y => y.BankAccount)
            .HasForeignKey(y => y.BankAccountId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.OutTransfers)
            .WithOne(y => y.ToBankAccount)
            .HasForeignKey(y => y.ToBankAccountId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.InTransfers)
            .WithOne(y => y.FromBankAccount)
            .HasForeignKey(y => y.FromBankAccountId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.SetPropertyAccessModeField
            (x => x.User, BankAccount.UserField);
        builder.SetPropertyAccessModeField
            (x => x.FinancialTransactions, BankAccount.FinancialTransactionsField);
        builder.SetPropertyAccessModeField
            (x => x.OutTransfers, BankAccount.OutTransfersField);
        builder.SetPropertyAccessModeField(x => x.InTransfers, BankAccount.InTransfersField);
    }
}