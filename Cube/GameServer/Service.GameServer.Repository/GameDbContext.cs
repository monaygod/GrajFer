using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Domain.PlayerAggregate;
using Service.GameServer.Domain.RoomAggregate;
using Service.GameServer.Domain.ValueObject;

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
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player>  Players { get; set; }


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

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
                entity.HasKey(x => x.Id).HasAnnotation("Sqlite:Autoincrement", true);
                
                entity.Property(x => x.RoomName)
                    .HasField("_roomName")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
                entity.HasIndex(x => x.RoomName)
                    .IsUnique();
                
                entity.OwnsOne(x => x.Password);
                entity.Navigation(x=>x.Password).Metadata.SetField("_password");
                
                entity.OwnsOne(x => x.Game,
                    gameEntity =>
                    {
                        gameEntity.OwnsMany(x => x.StaticFields,
                            childEntity =>
                            {
                                childEntity.HasKey("Id").HasAnnotation("Sqlite:Autoincrement", true);
                                childEntity.OwnsMany(x => x.ActiveElements,
                                    childChildEntity =>
                                    {
                                        childChildEntity.HasKey("Id").HasAnnotation("Sqlite:Autoincrement", true);
                                    });
                            });
                    });
                entity.Navigation(x=>x.Game).Metadata.SetField("_game");
                
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
               
                entity.HasOne<Player>().WithMany().HasForeignKey(x => x.Host);
                entity.HasMany(x=>x.Players).WithMany(x=>x.Rooms);
            });
            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");
                entity.HasKey(x => x.Id).HasAnnotation("Sqlite:Autoincrement", true);
                entity.Property(x => x.PlayerId)
                    .HasField("_playerId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
                entity.HasIndex(x => x.PlayerId)
                    .IsUnique();
                
                entity.Property(x => x.SignalrConnectionId)
                    .HasField("_signalrConnectionId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
                entity.HasIndex(x => x.SignalrConnectionId)
                    .IsUnique();
            });
        }

    }
}
