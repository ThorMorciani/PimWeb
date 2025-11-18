using AIssist.Domain.Entities;
using AIssist.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIssist.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Profiles> Profiles => Set<Profiles>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<RootCause> RootCauses => Set<RootCause>();
        public DbSet<Tickets> Tickets => Set<Tickets>();
        public DbSet<Logs> Logs => Set<Logs>();

        public void Atualizar<T>(T entity, T updatedEntity) where T : class
        {
            Entry(entity).CurrentValues.SetValues(updatedEntity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.ToTable("Logs");

                entity.HasKey(e => e.Id)
                      .HasName("PK_Logs");

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasColumnType("datetimeoffset(7)");
            });

            modelBuilder.Entity<Profiles>(entity =>
            {
                entity.ToTable("Profiles");

                entity.HasKey(e => e.Id)
                      .HasName("PK_Profiles");

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.ProfileName)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("Profile")
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasColumnType("datetimeoffset(7)")
                      .HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.CreatedBy)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.UpdatedAt)
                      .IsRequired()
                      .HasColumnType("datetime2(7)");

                entity.Property(e => e.UpdatedBy)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Active)
                      .IsRequired()
                      .HasColumnType("bit")
                      .HasDefaultValue(true);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id)
                      .HasName("PK_Users");

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasColumnType("datetimeoffset(7)");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.UpdatedAt)
                      .IsRequired()
                      .HasColumnType("datetime2(7)");

                entity.Property(e => e.Active)
                      .IsRequired()
                      .HasColumnType("bit");

                entity.Property(e => e.RefreshToken)
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.RefreshTokenExpiryTime)
                      .HasColumnType("datetime2(7)");

                entity.Property(e => e.ProfileId)
                      .IsRequired()
                      .HasColumnType("bigint");

                entity.HasOne(e => e.Profile)
                      .WithMany()
                      .HasForeignKey(e => e.ProfileId)
                      .HasConstraintName("FK_Users_Profiles");
            });

            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.ToTable("Tickets");

                entity.HasKey(e => e.Id)
                      .HasName("PK_Tickets");

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.Solution)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.TicketNumber)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");

                entity.Property(e => e.AssigneeId)
                      .IsRequired(false)
                      .HasColumnType("bigint");

                entity.Property(e => e.ReporterId)
                      .IsRequired()
                      .HasColumnType("bigint");

                entity.Property(e => e.RootCauseId)
                      .IsRequired()
                      .HasColumnType("bigint");

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasConversion<int>()
                      .HasColumnName("Status")
                      .HasColumnType("int");

                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasColumnType("datetimeoffset(7)")
                      .HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.UpdatedAt)
                      .IsRequired()
                      .HasColumnType("datetime2(7)");

                entity.HasOne(e => e.Assignee)
                      .WithMany()
                      .HasForeignKey(e => e.AssigneeId)
                      .HasConstraintName("FK_Tickets_Assignee")
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Reporter)
                      .WithMany()
                      .HasForeignKey(e => e.ReporterId)
                      .HasConstraintName("FK_Tickets_Reporter")
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.RootCause)
                      .WithMany(rc => rc.Tickets)
                      .HasForeignKey(e => e.RootCauseId)
                      .HasConstraintName("FK_Tickets_RootCause")
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<RootCause>(entity =>
            {
                entity.ToTable("RootCauses");

                entity.HasKey(e => e.Id)
                      .HasName("PK_RootCauses");

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.RootCauseName)
                      .IsRequired()
                      .HasColumnName("RootCause")
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Criticality)
                      .IsRequired()
                      .HasConversion<int>()
                      .HasColumnName("Criticality")
                      .HasColumnType("int");

                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasColumnType("datetimeoffset(7)")
                      .HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.CreatedBy)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.UpdatedAt)
                      .IsRequired()
                      .HasColumnType("datetime2(7)");

                entity.Property(e => e.UpdatedBy)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Active)
                      .IsRequired()
                      .HasColumnType("bit")
                      .HasDefaultValue(true);

                entity.HasIndex(e => e.RootCauseName)
                      .IsUnique()
                      .HasDatabaseName("UQ_RootCauses_RootCause");
            });
        }

    }
}

