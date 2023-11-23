using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
        {
            public void Configure(EntityTypeBuilder<DetallePedido> entity)
            {
                entity.HasKey(e => new { e.IdPedido, e.IdProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("detalle_pedido");

                entity.HasIndex(e => e.IdProducto, "id_producto");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
                entity.Property(e => e.IdProducto)
                    .HasMaxLength(15)
                    .HasColumnName("id_producto");
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
                entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");
                entity.Property(e => e.PrecioUnidad)
                    .HasPrecision(15, 2)
                    .HasColumnName("precio_unidad");

                entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalle_pedido_ibfk_1");

                entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalle_pedido_ibfk_2");
            }
        }
}