using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Transfer"/>
/// </summary>
internal class TransferConfiguration : EntityBaseConfiguration<Transfer>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("transfer", "public")
            .HasComment("Перевод");
        
        builder.Property(p => p.Amount)
            .HasComment("Сумма перевода")
            .IsRequired();
        
        builder.Property(p => p.FromBankAccountId)
            .HasComment("Идентификатор счета, с которого был отправлен перевод")
            .IsRequired();
        
        builder.Property(p => p.ToBankAccountId)
            .HasComment("Идентификатор счета, на который был отправлен перевод")
            .IsRequired();
        
        builder.HasOne(x => x.FromBankAccount)
            .WithMany(y => y.InTransfers)
            .HasForeignKey(x => x.FromBankAccountId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.ToBankAccount)
            .WithMany(y => y.OutTransfers)
            .HasForeignKey(x => x.ToBankAccountId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.SetPropertyAccessModeField(x => x.FromBankAccount, Transfer.FromBankAccountField);
        builder.SetPropertyAccessModeField(x => x.ToBankAccount, Transfer.ToBankAccountField);
    }
}