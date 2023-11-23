using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.IdGama, "id_gama");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .HasColumnName("id");
            entity.Property(e => e.CantidadEnStock).HasColumnName("cantidad_en_stock");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Dimensiones)
                .HasMaxLength(25)
                .HasColumnName("dimensiones");
            entity.Property(e => e.IdGama)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("id_gama");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(70)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioProveedor)
                .HasPrecision(15, 2)
                .HasColumnName("precio_proveedor");
            entity.Property(e => e.PrecioVenta)
                .HasPrecision(15, 2)
                .HasColumnName("precio_venta");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .HasColumnName("proveedor");

            entity.HasOne(d => d.IdGamaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdGama)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("producto_ibfk_1");
        }
    }
}