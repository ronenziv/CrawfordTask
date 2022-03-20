using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CrawfordTask.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CrawfordTask.Common.Helpers
{
    public partial class InterviewContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public InterviewContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<FileActivityLinks> FileActivityLinks { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<LegacyActivities> LegacyActivities { get; set; }
        public virtual DbSet<LegacyActivityCategories> LegacyActivityCategories { get; set; }
        public virtual DbSet<LegacyActivityTypes> LegacyActivityTypes { get; set; }
        public virtual DbSet<LossTypes> LossTypes { get; set; }
        public virtual DbSet<PartyClaimRole> PartyClaimRole { get; set; }
        public virtual DbSet<PartyTypes> PartyTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // connect to sql server database
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activities>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActivityDetails)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ActivityTypeCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LegacyActivityId).HasMaxLength(50);

                entity.Property(e => e.PartyType)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Claims>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LossLoc).HasMaxLength(200);

                entity.Property(e => e.Policy).HasMaxLength(100);

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Created)
                    .WithMany(p => p.ClaimsCreated)
                    .HasForeignKey(d => d.CreatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__CreatedI__10416098");

                entity.HasOne(d => d.LastUpdated)
                    .WithMany(p => p.ClaimsLastUpdated)
                    .HasForeignKey(d => d.LastUpdatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__LastUpda__113584D1");

                entity.HasOne(d => d.LossAdjuster)
                    .WithMany(p => p.ClaimsLossAdjuster)
                    .HasForeignKey(d => d.LossAdjusterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__LossAdju__0F4D3C5F");

                entity.HasOne(d => d.LossType)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.LossTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__LossType__0E591826");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BodyText).HasColumnType("ntext");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PartyTypeCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RecipientTo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SentBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SentTime).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FileActivityLinks>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LegacyActivityId).HasMaxLength(50);
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BlobName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PublicId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<LegacyActivities>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AdhocOrPartyInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CategoryInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LegacyActivityCategories>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LegacyActivityTypes>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CategoryCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LossTypes>(entity =>
            {
                entity.HasKey(e => e.LossTypeId)
                    .HasName("PK__LossType__757E35340BE95B32");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LossTypeCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.LossTypeDescription)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PartyClaimRole>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AdhocOrPartyInd)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PartyReference).HasMaxLength(20);

                entity.Property(e => e.PartyTypeCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Telephone).HasMaxLength(35);
            });

            modelBuilder.Entity<PartyTypes>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C24BD5454");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
