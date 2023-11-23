using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.IdJefe, "id_jefe");

            entity.HasIndex(e => e.IdOficina, "id_oficina");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Apellido1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .HasColumnName("apellido2");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.IdJefe).HasColumnName("id_jefe");
            entity.Property(e => e.IdOficina)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("id_oficina");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .HasColumnName("puesto");

            entity.HasOne(d => d.IdJefeNavigation).WithMany(p => p.InverseIdJefeNavigation)
                .HasForeignKey(d => d.IdJefe)
                .HasConstraintName("empleado_ibfk_2");

            entity.HasOne(d => d.IdOficinaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdOficina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empleado_ibfk_1");
        }
    }
}