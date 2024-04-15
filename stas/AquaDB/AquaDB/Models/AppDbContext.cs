using AquaDB.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media.Media3D;

namespace AquaDB.Database
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<MeasurementType> MeasurementTypes { get; set; }
        public DbSet<Probe> Probes { get; set; }
        public DbSet<ProbeData> ProbeDatas { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        public AppDbContext() : base()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local); Database=SHevelevDB; Integrated Security=true;");
            //optionsBuilder.UseSqlServer(@"Data Source=DBSRV\AG2023; Initial Catalog=SHevelevAquaDB; Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            List<AppUser> users = new()
            {
                new() { Id = 1, Name = "1", Password = "1", Email = "qwe", Role = Role.Administrator },
                new() { Id = 2, Name = "2", Password = "2", Email = "asd" }
            };
            List<MeasurementType> measurementTypes = new()
            {
                new() { Id = 1, Name = "asd" },
                new() { Id = 2, Name = "qwe" }
            };
            List<Probe> probes = new()
            {
                new() { Id = 1, Title = "rty" },
                new() { Id = 2, Title = "fgh" }
            };
            List<Project> projects = new()
            {
                new() { Id = 1, Title = "zxc" },
                new() { Id = 2, Title = "vbn" }
            };
            List<Measurement> measurements = new()
            {
                new() { Id = 1, Date = DateTime.Now.AddDays(-2), Location = "qwe 13/5", Depth = 13, MeasurementTypeId = 1, ProjectId = 1 },
                new() { Id = 2, Date = DateTime.Now.AddDays(-1), Location = "asd 1/2", Depth = 49, MeasurementTypeId = 2, ProjectId = 2 }
            };
            List<ProbeData> probeData = new()
            {
                new() { Id = 1, ProbeId = 1, MeasurementId = 1, Value = 40 },
                new() { Id = 2, ProbeId = 2, MeasurementId = 2, Value = 15 }
            };
            List<UserProject> userProjects = new()
            {
                new() { Id = 1, ProjectId = 1, UserId = 1 },
                new() { Id = 2, ProjectId = 2, UserId = 2 }
            };

            modelBuilder.Entity<AppUser>().HasData(users);
            modelBuilder.Entity<MeasurementType>().HasData(measurementTypes);
            modelBuilder.Entity<Probe>().HasData(probes);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<Measurement>().HasData(measurements);
            modelBuilder.Entity<ProbeData>().HasData(probeData);
            modelBuilder.Entity<UserProject>().HasData(userProjects);
        }
    }
}