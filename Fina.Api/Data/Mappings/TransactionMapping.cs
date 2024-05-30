using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnType("SMALLINT");

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("MONEY");

            builder.Property(t => t.CreatedAt)
                .IsRequired(); //Sem especificar tipo ele já define data como tipo DATETIME2, o mais otimizado para datas
            
            builder.Property(t => t.PaidOrReceivedAt)
                .IsRequired(false);
            
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
        }
    }
}