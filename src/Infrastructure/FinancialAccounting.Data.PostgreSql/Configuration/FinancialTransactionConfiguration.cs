using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="FinancialTransaction"/>
/// </summary>
internal class FinancialTransactionConfiguration : EntityBaseConfiguration<FinancialTransaction>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<FinancialTransaction> builder)
    {
        builder.ToTable("financial_transaction", "public")
            .HasComment("Финансовая операция");

        builder.Property(p => p.Amount)
            .HasComment("Сумма операции")
            .IsRequired();

        builder.Property(p => p.Comment)
            .HasComment("Комментарий");
        
        builder.Property(p => p.Type)
            .HasComment("Тип операции")
            .IsRequired();

        builder.Property(p => p.ActualDate)
            .HasComment("Фактическая дата совершения операции")
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .HasComment("Идентификатор категории")
            .IsRequired();

        builder.Property(p => p.BankAccountId)
            .HasComment("Идентификатор счета")
            .IsRequired();
        
        builder.HasOne(x => x.Category)
            .WithMany(y => y.FinancialTransactions)
            .HasForeignKey(x => x.CategoryId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.BankAccount)
            .WithMany(y => y.FinancialTransactions)
            .HasForeignKey(x => x.BankAccountId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Photos)
            .WithOne(y => y.FinancialTransaction)
            .HasForeignKey(y => y.FinancialTransactionId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.SetPropertyAccessModeField
            (x => x.BankAccount, FinancialTransaction.BankAccountField);
        builder.SetPropertyAccessModeField
            (x => x.Category, FinancialTransaction.CategoryField);
        builder.SetPropertyAccessModeField
            (x => x.Photos, FinancialTransaction.PhotosField);
    }
}