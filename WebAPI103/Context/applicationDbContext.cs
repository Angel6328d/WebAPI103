using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAPI103.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        // Modelos a mapear en la base de datos
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Rol> rols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Angel",
                    Username = "Chocorroll",
                    Password = "123456",
                    FkRol = 1
                }
            );

            // Configuración de la entidad Rol
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRol = 1,
                    Nombre = "SA",
                    Estatus = true
                },
                new Rol
                {
                    PkRol = 2,
                    Nombre = "Admin",
                    Estatus = false
                }
            );
        }
    }
}