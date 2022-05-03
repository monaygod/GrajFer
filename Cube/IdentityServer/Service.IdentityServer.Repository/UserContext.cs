using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service.IdentityServer.Domain.UserAggregate;

#nullable disable

namespace Service.IdentityServer.Repository
{
    public partial class UserContext : DbContext
    {
        private static bool _created = false;
        public UserContext()
        {
            CreateDatabase();
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            CreateDatabase();
        }
        public virtual DbSet<User> Users { get; set; }
        

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
            
           modelBuilder.Entity<User>(entity =>
           {
               entity.ToTable("User");
               entity.HasKey(x => x.Id);
               entity.Property(x => x.UserName)
                   .HasField("_userName")
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();
               entity.HasIndex(x => x.UserName)
                   .IsUnique();
               
               entity.OwnsOne(x => x.Password);
               entity.Navigation(x=>x.Password).Metadata.SetField("_password");
               
               var refreshTokensConfig = entity.OwnsMany(x => x.RefreshTokens,
                   childEntity =>
                   {
                       childEntity.HasIndex(x => x.Token)
                           .IsUnique();
                       childEntity.HasKey("Id").HasAnnotation("Sqlite:Autoincrement", true);
                   });
               entity.Navigation(x => x.RefreshTokens).Metadata.SetField("_refreshTokens");

               var permissionConfig = entity.OwnsMany(x => x.Permission,
                   childEntity =>
                   {
                       childEntity.HasKey("Id").HasAnnotation("Sqlite:Autoincrement", true);
                   });
               entity.Navigation(x => x.Permission).Metadata.SetField("_userPermissions");
           });
        }

        private void CreateDatabase()
        {
            if (!_created)
            {
                _created = true;
                GC.Collect();
                Database.EnsureDeleted();
                Database.EnsureCreated();

                Users.Add(
                    new User(
                        Guid.NewGuid(),
                        "admin",
                        "admin",
                        new List<string>() { "sa" }));
                SaveChanges();
            }

        }

    }
}
