using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("CHAR(11)").IsRequired();
            builder.Property(p => p.Valor).IsRequired();

            //builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}