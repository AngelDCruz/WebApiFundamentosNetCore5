using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiFundamentos.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutorLibro>().HasKey(x => new { x.AutorId, x.LibroId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Autores> Autores {get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<AutorLibro> AutoresLibros { get; set; }
    }
}
