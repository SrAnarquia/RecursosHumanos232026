using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecursosHumanos.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aprobacion> Aprobacions { get; set; }

    public virtual DbSet<Base> Bases { get; set; }

    public virtual DbSet<CatalogoCurso> CatalogoCursos { get; set; }

    public virtual DbSet<CatalogoNivel> CatalogoNivels { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Correo> Correos { get; set; }

    public virtual DbSet<CursosPersona> CursosPersonas { get; set; }

    public virtual DbSet<DatosReclutamiento> DatosReclutamientos { get; set; }

    public virtual DbSet<DatosReclutamientoReflejo> DatosReclutamientoReflejos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DiasEmpleado> DiasEmpleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Estatus> Estatuses { get; set; }

    public virtual DbSet<EstatusCurso> EstatusCursos { get; set; }

    public virtual DbSet<Fuente> Fuentes { get; set; }

    public virtual DbSet<Incidente> Incidentes { get; set; }

    public virtual DbSet<Posicion> Posicions { get; set; }

    public virtual DbSet<Razone> Razones { get; set; }

    public virtual DbSet<RazonesIncidente> RazonesIncidentes { get; set; }

    public virtual DbSet<Reclutador> Reclutadors { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    public virtual DbSet<TipoCurso> TipoCursos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioEspejo> UsuarioEspejos { get; set; }

    public virtual DbSet<Vacacion> Vacacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aprobacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aprobaci__3214EC07B174EE6D");

            entity.ToTable("Aprobacion", "Vacaciones");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Base>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Base__3214EC074BC0B040");

            entity.ToTable("Base", "Reclutamiento");

            entity.Property(e => e.Base1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Base");
            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CatalogoCurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC075C39BCC2");

            entity.ToTable("CatalogoCursos", "Entrenamiento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(2555)
                .IsUnicode(false);
            entity.Property(e => e.Diploma)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.CatalogoCursos)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_CatalogoCursos_Departamento");

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.CatalogoCursos)
                .HasForeignKey(d => d.IdNivel)
                .HasConstraintName("FK_CatalogoCursos_Nivel");

            entity.HasOne(d => d.IdTipoCursoNavigation).WithMany(p => p.CatalogoCursos)
                .HasForeignKey(d => d.IdTipoCurso)
                .HasConstraintName("FK_CatalogoCursos_TipoCurso");
        });

        modelBuilder.Entity<CatalogoNivel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Catalogo__3214EC071484793E");

            entity.ToTable("CatalogoNivel", "Entrenamiento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC070917F9C5");

            entity.ToTable("Categoria", "Incidentes");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Correo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Correos__3214EC078A956CA9");

            entity.ToTable("Correos", "Vacaciones");

            entity.Property(e => e.Correo1)
                .HasMaxLength(255)
                .HasColumnName("Correo");
            entity.Property(e => e.Departamento)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CursosPersona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CursosPe__3214EC07689CEDE8");

            entity.ToTable("CursosPersonas", "Entrenamiento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Diploma)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.CursosPersonas)
                .HasForeignKey(d => d.IdEstatus)
                .HasConstraintName("FK_IdEstatus");
        });

        modelBuilder.Entity<DatosReclutamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Datos_Re__3214EC07A4DE686C");

            entity.ToTable("Datos_Reclutamiento", "Reclutamiento");

            entity.Property(e => e.Comentarios)
                .HasMaxLength(2555)
                .IsUnicode(false);
            entity.Property(e => e.FechaContacto)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBaseNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdBase)
                .HasConstraintName("FK_Base_Nombre");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Empresa_Nombre");

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdEstatus)
                .HasConstraintName("FK_Estatus_Nombre");

            entity.HasOne(d => d.IdFuenteNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdFuente)
                .HasConstraintName("FK_Fuente_Nombre");

            entity.HasOne(d => d.IdPosicionNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdPosicion)
                .HasConstraintName("FK_Posicion_Nombre");

            entity.HasOne(d => d.IdReclutadorNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdReclutador)
                .HasConstraintName("FK_Reclutador_Nombre");

            entity.HasOne(d => d.IdSexoNavigation).WithMany(p => p.DatosReclutamientos)
                .HasForeignKey(d => d.IdSexo)
                .HasConstraintName("FK_Sexo_Nombre");
        });

        modelBuilder.Entity<DatosReclutamientoReflejo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Datos_Re__3214EC07155A07DC");

            entity.ToTable("Datos_Reclutamiento_Reflejo", "Reclutamiento");

            entity.Property(e => e.Base)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Comentarios)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fuente)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Posicion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Reclutador)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC07C52529C5");

            entity.ToTable("Departamento", "Entrenamiento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DiasEmpleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiasEmpl__3214EC070106B0AD");

            entity.ToTable("DiasEmpleados", "Vacaciones");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresa__3214EC0716581833");

            entity.ToTable("Empresa", "Reclutamiento");

            entity.Property(e => e.Empresa1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Empresa");
            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Estatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estatus__3214EC072F975721");

            entity.ToTable("Estatus", "Reclutamiento");

            entity.Property(e => e.Estatus1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Estatus");
            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EstatusCurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstatusC__3214EC07D79AF881");

            entity.ToTable("EstatusCurso", "Entrenamiento");

            entity.Property(e => e.Estatus)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Fuente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fuente__3214EC0784036524");

            entity.ToTable("Fuente", "Reclutamiento");

            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Fuente1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Fuente");
        });

        modelBuilder.Entity<Incidente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Incident__3214EC07F48101D0");

            entity.ToTable("Incidente", "Incidentes");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Evidencia)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaIncidente).HasColumnType("datetime");
            entity.Property(e => e.NombreIncidente)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRazonNavigation).WithMany(p => p.Incidentes)
                .HasForeignKey(d => d.IdRazon)
                .HasConstraintName("FK_RAZON_INCIDENTE");
        });

        modelBuilder.Entity<Posicion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posicion__3214EC07E21C48C7");

            entity.ToTable("Posicion", "Reclutamiento");

            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Posicion1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Posicion");
        });

        modelBuilder.Entity<Razone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Razones__3214EC07CDC621BC");

            entity.ToTable("Razones", "Vacaciones");

            entity.Property(e => e.Razon)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RazonesIncidente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RazonesI__3214EC07A803D0F1");

            entity.ToTable("RazonesIncidentes", "Incidentes");

            entity.Property(e => e.Razon)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reclutador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reclutad__3214EC0739300128");

            entity.ToTable("Reclutador", "Reclutamiento");

            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sexo__3214EC072F6FFB1B");

            entity.ToTable("Sexo", "Reclutamiento");

            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Sexo1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Sexo");
        });

        modelBuilder.Entity<TipoCurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoCurs__3214EC075E7746E7");

            entity.ToTable("TipoCurso", "Entrenamiento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(2555)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
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
            entity.Property(e => e.TipoUsuario)
                .HasDefaultValue(1)
                .HasColumnName("tipo_usuario");
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

        modelBuilder.Entity<Vacacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vacacion__3214EC078BB1C7D3");

            entity.ToTable("Vacacion", "Vacaciones");

            entity.Property(e => e.Departamento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Detalles)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAprobadoNavigation).WithMany(p => p.Vacacions)
                .HasForeignKey(d => d.IdAprobado)
                .HasConstraintName("FK_APROBACIONES");

            entity.HasOne(d => d.IdRazonNavigation).WithMany(p => p.Vacacions)
                .HasForeignKey(d => d.IdRazon)
                .HasConstraintName("FK_RAZONES");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
