using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="User"/>
/// </summary>
internal class UserConfiguration : EntityBaseConfiguration<User>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "public")
            .HasComment("Пользователь");

        builder.Property(p => p.Login)
            .HasComment("Логин")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasComment("Электронная почта")
            .IsRequired();

        builder.Property(p => p.PasswordHash)
            .HasComment("Хеш пароля")
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasComment("Телефон");

        builder.HasMany(x => x.BankAccounts)
            .WithOne(y => y.User)
            .HasForeignKey(y => y.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.SetPropertyAccessModeField(x => x.BankAccounts, User.BankAccountsField);
    }
}