using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.Models;

public partial class TuDbContext : DbContext
{
    public TuDbContext()
    {
    }

    public TuDbContext(DbContextOptions<TuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Base> Bases { get; set; }

    public virtual DbSet<DatosReclutamiento> DatosReclutamientos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estatus> Estatuses { get; set; }

    public virtual DbSet<Fuente> Fuentes { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioEspejo> UsuarioEspejos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Base>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Base__3214EC270005CCDC");

            entity.ToTable("Base", "Reclutamiento");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Base1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Base");
            entity.Property(e => e.FechaCreación).HasColumnType("datetime");
        });

        modelBuilder.Entity<DatosReclutamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Datos_Re__3214EC07EFA65700");

            entity.ToTable("Datos_Reclutamiento", "Reclutamiento");

            entity.Property(e => e.Comentarios)
                .HasMaxLength(2555)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreación).HasColumnType("datetime");
            
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBaseNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdBase)
                .HasConstraintName("FK_Base_Nombre");

            entity.HasOne(d => d.IdPosicionNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdPosicion)
                .HasConstraintName("FK_Posicion_Nombre");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Empresa_Nombre");

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdEstatus)
                .HasConstraintName("FK_Estatus_Nombre");

            entity.HasOne(d => d.IdFuenteNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdFuente)
                .HasConstraintName("FK_Fuente_Nombre");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresa__3214EC076F77A08B");

            entity.ToTable("Empresa", "Reclutamiento");

            entity.Property(e => e.Empresa1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Empresa");
            entity.Property(e => e.FechaCreación).HasColumnType("datetime");
        });

        modelBuilder.Entity<Estatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estatus__3214EC0720ED1FD3");

            entity.ToTable("Estatus", "Reclutamiento");

            entity.Property(e => e.Estatus1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Estatus");
            entity.Property(e => e.FechaCreación).HasColumnType("datetime");
        });

        modelBuilder.Entity<Fuente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fuente__3214EC07C5162639");

            entity.ToTable("Fuente", "Reclutamiento");

            entity.Property(e => e.FechaCreación).HasColumnType("datetime");
            entity.Property(e => e.Fuente1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Fuente");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Puesto__3214EC07FDFCCFF3");

            entity.ToTable("Puesto", "Reclutamiento");

            entity.Property(e => e.FechaCreación).HasColumnType("datetime");
            entity.Property(e => e.Puesto1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Puesto");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3214EC0764D14EAE");

            entity.ToTable("usuarios", "Usuario");

            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.EsAdministrador).HasColumnName("es_administrador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        modelBuilder.Entity<UsuarioEspejo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UsuarioEspejo", "Usuario");

            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.EsAdministrador)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("es_administrador");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Usuario)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
