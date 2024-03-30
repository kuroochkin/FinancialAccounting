using FinancialAccounting.Dara.PostgreSql.Extensions;
using FinancialAccounting.Data.PostgreSql.Configuration;
using FinancialAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = FinancialAccounting.Domain.Entities.File;

namespace FinancialAccounting.Dara.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Photo"/>
/// </summary>
internal class PhotoConfiguration : EntityBaseConfiguration<Photo>
{
    /// <inheritdoc/>
    public override void ConfigureChild(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("photo", "public")
            .HasComment("Фотография");
        
        builder.Property(p => p.FinancialTransactionId)
            .HasComment("Идентификатор финансовой операции")
            .IsRequired();
        
        builder.Property(p => p.FileId)
            .HasComment("Идентификатор файла")
            .IsRequired();

        builder.HasOne(x => x.FinancialTransaction)
            .WithMany(y => y.Photos)
            .HasForeignKey(x => x.FinancialTransactionId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(x => x.File)
            .WithOne(y => y.Photo)
            .HasForeignKey<Photo>(x => x.FileId)
            .HasPrincipalKey<File>(y => y.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.SetPropertyAccessModeField(x => x.File, Photo.FileField);
        builder.SetPropertyAccessModeField(x => x.FinancialTransaction, Photo.FinancialTransactionField);
    }
}