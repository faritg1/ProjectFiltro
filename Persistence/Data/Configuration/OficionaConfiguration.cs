using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class OficionaConfiguration : IEntityTypeConfiguration<Oficina>
    {
        public void Configure(EntityTypeBuilder<Oficina> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("oficina");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.LineaDireccion1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.Pais)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("telefono");
        }
    }
}