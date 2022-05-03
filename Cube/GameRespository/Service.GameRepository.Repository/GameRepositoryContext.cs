using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service.GameRepository.Domain.GameFileAggregate;

#nullable disable

namespace Service.GameRepository.Repository
{
    public partial class GameRepositoryContext : DbContext
    {
        private static bool _created = false;
        public GameRepositoryContext()
        {
            if (!_created)
            {
                _created = true;
                GC.Collect();
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public GameRepositoryContext(DbContextOptions<GameRepositoryContext> options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                GC.Collect();
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        public virtual DbSet<GameFile> Games { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder
                {
                    DataSource = "Data\\Database.db",
                    Pooling = false,
                };
                var connectionString = connectionStringBuilder.ToString();
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

           modelBuilder.Entity<GameFile>(entity =>
           {
               entity.ToTable("Games");
               entity.HasKey(x => x.Id);
               
               entity.Property(x => x.GameName)
                   .HasField("_gameName")
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();
               
               entity.HasIndex(e => e.GameName).IsUnique();

               entity.Property(x => x.FilePath)
                   .HasField("_filePath")
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();
           });

        }

    }
}
