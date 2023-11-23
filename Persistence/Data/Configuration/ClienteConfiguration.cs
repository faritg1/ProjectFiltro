using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.IdEmpleado, "id_empleado");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApellidoContacto)
                .HasMaxLength(30)
                .HasColumnName("apellido_contacto");
            entity.Property(e => e.Ciudad)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Fax)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.LimiteCredito)
                .HasPrecision(15, 2)
                .HasColumnName("limite_credito");
            entity.Property(e => e.LineaDireccion1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.NombreCliente)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(30)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("cliente_ibfk_1");
        }
    }
}