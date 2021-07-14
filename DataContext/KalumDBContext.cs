using System.IO;
using ControlDeColegio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControlDeColegio.DataContext
{
    public class KalumDBContext : DbContext
    {
        public DbSet<Alumno> Alumnos {get; set;}
        public DbSet<AsignacionAlumno> AsignacionAlumnos {get; set;}
        public DbSet<Carrera> Carreras {get; set;}
        public DbSet<Clase> Clases {get; set;}
        public DbSet<Horario> Horarios {get; set;}
        public DbSet<Instructor> Instructores {get; set;}
        public DbSet<Salon> Salones {get; set;}
        public DbSet<Usuarios> Usuarios {get; set;}

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
            modelBuilder.Entity<AsignacionAlumno>()
            .ToTable("AsignacionAlumnos")
            .HasKey(a => new {a.AsignacionId});    
            modelBuilder.Entity<Carrera>()
            .ToTable("CarrerasTecnicas")
            .HasKey(a => new {a.CarreraId});
            modelBuilder.Entity<Clase>()
            .ToTable("Clases")
            .HasKey(a => new {a.ClaseId});
            modelBuilder.Entity<Horario>()
            .ToTable("Horarios")
            .HasKey(a => new {a.HorarioId});
            modelBuilder.Entity<Instructor>()
            .ToTable("Instructores")
            .HasKey(a => new {a.InstructorId});
            modelBuilder.Entity<Rol>()
            .ToTable("RolesApp")
            .HasKey(a => new {a.Id});
            modelBuilder.Entity<Salon>()
                .ToTable("Salones")
                .HasKey(a => new { a.SalonId });
        }
    }
}