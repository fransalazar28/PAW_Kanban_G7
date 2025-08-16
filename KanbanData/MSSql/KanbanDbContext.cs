using Microsoft.EntityFrameworkCore;
using K.Models;

namespace K.Data.MSSql
{
    public partial class KanbanDbContext : DbContext
    {
        public KanbanDbContext() { }
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options) { }

        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Tablero> Tableros { get; set; } = null!;
        public virtual DbSet<Columna> Columnas { get; set; } = null!;
        public virtual DbSet<Historia> Historias { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Etiqueta> Etiquetas { get; set; } = null!;
        public virtual DbSet<HistoriaEtiqueta> HistoriaEtiquetas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning Considera mover la cadena de conexión a appsettings.json
            => optionsBuilder.UseSqlServer(
                "Server=localhost;Database=KanbanDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.NombreUsuario).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");
            });


 
            modelBuilder.Entity<Tablero>(entity =>
            {
                entity.ToTable("Tableros");
                entity.HasKey(e => e.TableroId);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Tableros)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });



            // Columnas
            modelBuilder.Entity<Columna>(entity =>
            {
                entity.ToTable("Columnas");
                entity.HasKey(e => e.ColumnaId);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Orden).IsRequired();

                entity.HasOne(e => e.Tablero)
                      .WithMany(t => t.Columnas)
                      .HasForeignKey(e => e.TableroId);
            });

            // Historias (tabla: HistoriasUsuario)
            modelBuilder.Entity<Historia>(entity =>
            {
                entity.ToTable("HistoriasUsuario");
                entity.HasKey(e => e.HistoriaId);

                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Descripcion).HasMaxLength(4000);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Orden).HasDefaultValue(int.MaxValue);

                // Columna (1) ──(N) Historias  → borrar columna borra historias
                entity.HasOne(e => e.Columna)
                      .WithMany(c => c.Historias)
                      .HasForeignKey(e => e.ColumnaId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Usuario (1) ──(N) Historias (Responsable opcional) → al borrar usuario, pone NULL
                entity.HasOne(e => e.Responsable)
                      .WithMany(u => u.HistoriasAsignadas)
                      .HasForeignKey(e => e.ResponsableId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Índice para ordenar dentro de la columna
                entity.HasIndex(e => new { e.ColumnaId, e.Orden });
            });

            // Comentarios
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("Comentarios");
                entity.HasKey(e => e.ComentarioId);
                entity.Property(e => e.Texto).IsRequired();
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("GETDATE()");

                // Historia (1) ──(N) Comentarios → borrar historia borra sus comentarios
                entity.HasOne(e => e.Historia)
                      .WithMany(h => h.Comentarios)
                      .HasForeignKey(e => e.HistoriaId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Usuario (1) ──(N) Comentarios → borrar usuario borra sus comentarios
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Comentarios)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Etiqueta>(entity =>
            {
                entity.ToTable("Etiquetas");
                entity.HasKey(e => e.EtiquetaId);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Color).HasMaxLength(20);
            });


            modelBuilder.Entity<HistoriaEtiqueta>(entity =>
            {
                entity.ToTable("HistoriaEtiqueta");
                entity.HasKey(e => new { e.HistoriaId, e.EtiquetaId });

                entity.HasOne(e => e.Historia)
                      .WithMany(h => h.HistoriasEtiquetas)
                      .HasForeignKey(e => e.HistoriaId);

                entity.HasOne(e => e.Etiqueta)
                      .WithMany(l => l.HistoriasEtiquetas)
                      .HasForeignKey(e => e.EtiquetaId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
