using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = FinancialAccounting.Domain.Entities.File;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="File"/>
/// </summary>
internal class FileConfiguration : EntityBaseConfiguration<File>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("file", "public")
            .HasComment("Файл");

        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.FileName).IsRequired();
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.ContentType);

        builder.Property(x => x.PhotoId).IsRequired();

        builder.HasOne(x => x.Photo)
            .WithOne(y => y.File)
            .HasForeignKey<Photo>(x => x.FileId)
            .HasPrincipalKey<File>(y => y.Id)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}