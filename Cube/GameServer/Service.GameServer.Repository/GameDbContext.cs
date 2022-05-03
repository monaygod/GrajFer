using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Domain.UserAggregate;

#nullable disable

namespace Service.GameServer.Repository
{
    public partial class GameDbContext : DbContext
    {
        private static bool _created = false;
        public GameDbContext()
        {
            if (_created) return;
            _created = true;
            GC.Collect();
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
            if (_created) return;
            _created = true;
            GC.Collect();
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Game> Users { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder
                {
                    DataSource = ":memory:",
                    Pooling = false,
                };
                var connectionString = connectionStringBuilder.ToString();
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
                entity.HasKey(x => x.Id);
                
                entity.Property(x => x.RoomName)
                    .HasField("_roomName")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
                entity.HasIndex(x => x.RoomName)
                    .IsUnique();
                
                entity.OwnsOne(x => x.Password);
                entity.Navigation(x=>x.Password).Metadata.SetField("_password");
                
                entity.Property(x => x.GameId)
                    .HasField("_gameId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
                entity.HasIndex(x => x.GameId)
                    .IsUnique();
                
                entity.Property(x => x.Description)
                    .HasField("_description")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
               
                entity.Property(x => x.MaxPlayers)
                    .HasField("_maxPlayers")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
               
                entity.OwnsOne(x => x.Host);
                entity.Navigation(x=>x.Host).Metadata.SetField("_host");
               
                var refreshTokensConfig = entity.OwnsMany(x => x.Players,
                    childEntity =>
                    {
                        childEntity.HasKey("Id").HasAnnotation("Sqlite:Autoincrement", true);
                    });
                entity.Navigation(x => x.Players).Metadata.SetField("_players");
            });
        }

    }
}
