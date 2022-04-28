using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service.IdentityServer.Domain.UserAggregate;
using Service.IdentityServer.Domain.ValueObject;

#nullable disable

namespace Service.IdentityServer.Repository
{
    public partial class UserContext : DbContext
    {
        private static bool _created = false;
        public UserContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                GC.Collect();
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        
        public virtual DbSet<NoTak> NoTaks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder
                {
                    DataSource = "w:\\Database.db",
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

            modelBuilder.Entity<NoTak>(
                entity =>
                {
                    entity.HasKey(x => x.Id);
                });
           modelBuilder.Entity<User>(entity =>
           {
               entity.ToTable("User");
               entity.HasKey(x => x.Id);

               entity.Property(x => x.UserName)
                   .HasField("_userName")
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();
               
               entity.OwnsOne(x => x.Password);
               entity.Navigation(x=>x.Password).Metadata.SetField("_password");
               
               var refreshTokensConfig = entity.OwnsMany(x => x.RefreshTokens);
               entity.Navigation(x => x.RefreshTokens).Metadata.SetField("_refreshTokens");

               var permissionConfig = entity.OwnsMany(x => x.Permission);
               entity.Navigation(x => x.Permission).Metadata.SetField("_userPermissions");
           });

        }

    }
}
