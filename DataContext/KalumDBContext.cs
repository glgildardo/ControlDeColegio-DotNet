using System.IO;
using ControlDeColegio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControlDeColegio.DataContext
{
    public class KalumDBContext : DbContext
    {
        public DbSet<Alumno> Alumnos {get; set;}

        public KalumDBContext(DbContextOptions<KalumDBContext> options)
            : base(options)
            {

            }
        
        public KalumDBContext(){
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
            .ToTable("Alumnos")
            .HasKey(a => new {a.Carne});
        }
    }
}