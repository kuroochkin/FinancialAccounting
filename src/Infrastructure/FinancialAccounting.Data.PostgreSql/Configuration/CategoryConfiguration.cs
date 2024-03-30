using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Category"/>
/// </summary>
internal class CategoryConfiguration : EntityBaseConfiguration<Category>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category", "public")
            .HasComment("Категория");
        
        builder.Property(p => p.Title)
            .HasComment("Название категории")
            .IsRequired();

        builder.HasMany(x => x.FinancialTransactions)
            .WithOne(y => y.Category)
            .HasForeignKey(y => y.CategoryId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.SetPropertyAccessModeField
            (x => x.FinancialTransactions, Category.FinancialTransactionsField);
    }
}