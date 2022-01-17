using Microsoft.EntityFrameworkCore;
using Service.IdentityServer.Domain.UserAggregate;
using Service.IdentityServer.Domain.ValueObject;
using Service.IdentityServer.Repository.Model;

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

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Password> Passwords { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=d:\Database.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "bo");
                entity.HasKey(x => x.Id);
                entity.HasMany(x=>x.Permission)

                entity.Property(e => e.).HasConversion(x=>x.EmailAdress)

                entity.Property(e => e.SupportName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email", "usr");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SupportName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<OrderRatingEmail>(entity =>
            {
                entity.ToTable("OrderRatingEmail", "bo");

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.HasOne(d => d.OrderRatingEmailStatus)
                    .WithMany(p => p.OrderRatingEmails)
                    .HasForeignKey(d => d.OrderRatingEmailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderRati__Order__14DBF883");

                entity.HasOne(d => d.OrderRatingEmailType)
                    .WithMany(p => p.OrderRatingEmails)
                    .HasForeignKey(d => d.OrderRatingEmailTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderRati__Order__11FF8BD8");
            });

            modelBuilder.Entity<OrderRatingEmailStatus>(entity =>
            {
                entity.ToTable("OrderRatingEmailStatus", "bo");

                entity.Property(e => e.SupportName)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<OrderRatingEmailType>(entity =>
            {
                entity.ToTable("OrderRatingEmailType", "bo");

                entity.Property(e => e.SupportName)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "bo");

                entity.Property(e => e.PermissionId).ValueGeneratedNever();

                entity.Property(e => e.ScopeName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0:0')");

                entity.Property(e => e.SupportName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_BoApplication");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier", "bo");

                entity.Property(e => e.SupplierId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ShortcutName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TaxIdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.TaxIdentificationNumberSuffix)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<SupplierWarehouseStock>(entity =>
            {
                entity.ToTable("SupplierWarehouseStock", "bo");

                entity.Property(e => e.GrossPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 3)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierWarehouseStocks)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WarehouseStock_Supplier");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "bo");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(1024);

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_BoUserStatus");
            });

            modelBuilder.Entity<UserApplication>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ApplicationId });

                entity.ToTable("UserApplication", "bo");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.UserApplications)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplication_BoApplication");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserApplications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplication_BoUser");
            });

            modelBuilder.Entity<UserApplicationConfiguration>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ApplicationId, e.ConfigurationTypeId });

                entity.ToTable("UserApplicationConfiguration", "bo");

                entity.Property(e => e.ConfigurationContent).IsRequired();

                entity.HasOne(d => d.ConfigurationType)
                    .WithMany(p => p.UserApplicationConfigurations)
                    .HasForeignKey(d => d.ConfigurationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplicationConfiguration_BoConfigurationType");

                entity.HasOne(d => d.UserApplication)
                    .WithMany(p => p.UserApplicationConfigurations)
                    .HasForeignKey(d => new { d.UserId, d.ApplicationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplicationConfiguration_BoUserApplication");
            });

            modelBuilder.Entity<UserApplicationPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ApplicationId, e.PermissionId });

                entity.ToTable("UserApplicationPermission", "bo");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserApplicationPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplicationPermission_BoPermission");

                entity.HasOne(d => d.UserApplication)
                    .WithMany(p => p.UserApplicationPermissions)
                    .HasForeignKey(d => new { d.UserId, d.ApplicationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserApplicationPermission_BoUserApplication");
            });

            modelBuilder.Entity<UserAuthorization>(entity =>
            {
                entity.HasKey(e => e.RefreshToken)
                    .HasName("PK__UserAuth__DEA298DBA43E6929");

                entity.ToTable("UserAuthorization", "bo");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.RefreshToken).HasMaxLength(64);

                entity.Property(e => e.AccessTokenCreationDate).HasColumnType("datetime");

                entity.Property(e => e.RefreshTokenCreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAuthorizations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAuthorization_User");
            });

            modelBuilder.Entity<UserConfiguration>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ConfigurationTypeId });

                entity.ToTable("UserConfiguration", "bo");

                entity.Property(e => e.ConfigurationContent).IsRequired();

                entity.HasOne(d => d.ConfigurationType)
                    .WithMany(p => p.UserConfigurations)
                    .HasForeignKey(d => d.ConfigurationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserConfiguration_BoConfigurationType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserConfigurations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserConfiguration_BoUser");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.ToTable("UserPermission", "bo");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_BoPermission");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_BoUser");
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.ToTable("UserRefreshToken", "bo");

                entity.Property(e => e.RefreshToken).IsRequired();

                entity.Property(e => e.RefreshTokenExpireTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus", "bo");

                entity.Property(e => e.UserStatusId).ValueGeneratedNever();

                entity.Property(e => e.SupportName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
