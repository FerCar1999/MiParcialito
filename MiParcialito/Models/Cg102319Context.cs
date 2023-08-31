using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MiParcialito.Models;

public partial class Cg102319Context : DbContext
{
    public Cg102319Context()
    {
    }

    public Cg102319Context(DbContextOptions<Cg102319Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Inscripcion> Inscripciones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:parcialservercarranza.database.windows.net,1433;Initial Catalog=CG102319;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.ToTable("Curso");
            entity.HasKey(e => e.CursoId).HasName("PK__Curso__7E0235D7A20324A7");
            entity.Property(e => e.CursoId)
                .HasColumnName("CursoID").ValueGeneratedOnAdd();
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.HasOne(e => e.User).WithMany(e => e.Cursos)
            .HasForeignKey(i => i.UserId).HasPrincipalKey(i => i.Id)
            .OnDelete(DeleteBehavior.ClientNoAction).HasConstraintName("FK__Curso__UserId__6754599E");
            entity.HasMany(e => e.Inscripciones).WithOne(e => e.Curso).HasForeignKey(e => e.CursoId);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F7682D8A01A4CF8");

            entity.Property(e => e.EstudianteId)
                .HasColumnName("EstudianteID").ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento);
            entity.HasMany(e => e.Inscripciones).WithOne(e => e.Estudiante).HasForeignKey(e => e.EstudianteId);

        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__168316B978DADB38");
            entity.Property(e => e.InscripcionId)
                .HasColumnName("InscripcionId").ValueGeneratedOnAdd();
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteId");
            entity.HasOne(i => i.Estudiante).WithMany(e => e.Inscripciones).HasForeignKey(i => i.EstudianteId)
            .HasPrincipalKey(e=>e.EstudianteId).OnDelete(DeleteBehavior.ClientNoAction).HasConstraintName("FK__Inscripci__Estud__619B8048");
            entity.Property(e => e.CursoId).HasColumnName("CursoId");
            entity.HasOne(i => i.Curso).WithMany(e => e.Inscripciones).HasForeignKey(i => i.CursoId)
            .HasPrincipalKey(e => e.CursoId).OnDelete(DeleteBehavior.ClientNoAction).HasConstraintName("FK__Inscripci__Curso__60A75C0F");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");
            entity.HasKey(e => e.Id).HasName("PK__UserType__3214EC07C8A3CFC4");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.HasMany(e => e.Users).WithOne(e => e.UserType).HasForeignKey(i=>i.UserTypeId).HasPrincipalKey(i => i.Id)
            .OnDelete(DeleteBehavior.ClientNoAction).HasConstraintName("FK__User__UserTypeId__66603565");
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0705F8F134");

            entity.Property(e => e.Id)
                .HasColumnName("Id").ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Edad).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeId");
            entity.HasOne(e=>e.UserType).WithMany(e=>e.Users)
            .HasForeignKey(i=>i.UserTypeId).HasPrincipalKey(i=>i.Id)
            .OnDelete(DeleteBehavior.ClientNoAction).HasConstraintName("FK__User__UserTypeId__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
