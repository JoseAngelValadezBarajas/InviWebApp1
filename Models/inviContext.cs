using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace inviWebApp.Models
{
    public partial class inviContext : DbContext
    {
        public inviContext()
        {
        }

        public inviContext(DbContextOptions<inviContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alcaldium> Alcaldia { get; set; } = null!;
        public virtual DbSet<Habitar> Habitars { get; set; } = null!;
        public virtual DbSet<Propietario> Propietarios { get; set; } = null!;
        public virtual DbSet<Viviendum> Vivienda { get; set; } = null!;
        public virtual DbSet<login> Login { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3333;database=invi;uid=root;pwd=admin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Alcaldium>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PRIMARY");

                entity.ToTable("alcaldia");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.Clave).HasColumnName("clave");

                entity.Property(e => e.Estado)
                    .HasMaxLength(45)
                    .HasColumnName("estado");

                entity.Property(e => e.Poblacion).HasColumnName("poblacion");
            });

            modelBuilder.Entity<Habitar>(entity =>
            {
                entity.ToTable("habitar");

                entity.HasIndex(e => e.Curp, "habitarcurp_idx");

                entity.HasIndex(e => e.Vivienda, "habitarviviend_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Curp)
                    .HasMaxLength(45)
                    .HasColumnName("curp");

                entity.Property(e => e.Vivienda).HasColumnName("vivienda");

                entity.HasOne(d => d.CurpNavigation)
                    .WithMany(p => p.Habitars)
                    .HasForeignKey(d => d.Curp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("propcurp");

                entity.HasOne(d => d.ViviendaNavigation)
                    .WithMany(p => p.Habitars)
                    .HasForeignKey(d => d.Vivienda)
                    .HasConstraintName("vivienda");
            });

            modelBuilder.Entity<Propietario>(entity =>
            {
                entity.HasKey(e => e.Curp)
                    .HasName("PRIMARY");

                entity.ToTable("propietario");

                entity.Property(e => e.Curp)
                    .HasMaxLength(45)
                    .HasColumnName("curp");

                entity.Property(e => e.Apellidomaterno)
                    .HasMaxLength(45)
                    .HasColumnName("apellidomaterno");

                entity.Property(e => e.Apellidopaterno)
                    .HasMaxLength(45)
                    .HasColumnName("apellidopaterno");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(12)
                    .HasColumnName("sexo");
            });

            modelBuilder.Entity<Viviendum>(entity =>
            {
                entity.ToTable("vivienda");

                entity.HasIndex(e => e.Alcaldia, "alcaldiavivienda_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Alcaldia)
                    .HasMaxLength(45)
                    .HasColumnName("alcaldia");

                entity.Property(e => e.Dirreccion)
                    .HasMaxLength(45)
                    .HasColumnName("dirreccion");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(45)
                    .HasColumnName("tipo");

                entity.HasOne(d => d.AlcaldiaNavigation)
                    .WithMany(p => p.Vivienda)
                    .HasForeignKey(d => d.Alcaldia)
                    .HasConstraintName("alcaldiavivienda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
