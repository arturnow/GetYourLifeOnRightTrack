using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using WasterDAL.Model;

namespace WasterDAL
{
    public class WasteContext : DbContext
    {
        public WasteContext()
            : base("WasteContext")
        {
        }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<Pattern> Patterns { get; set; }

        public DbSet<TrackRecord> TrackRecords { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<Period> Periods { get; set; }

        public DbSet<WasteStatistic> WasteStatistics { get; set; }

        public DbSet<BatchLog> BatchLogs { get; set; }

        public DbSet<SocialProfile> SocialProfiles { get; set; }
        public DbSet<Relation> Relations { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserInGroup> UserInGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            SetupIdentity(modelBuilder);
            SetupTrackRecord(modelBuilder);
            SetupPattern(modelBuilder);
            SetupMessage(modelBuilder);
            SetupGoal(modelBuilder);
            SetupTask(modelBuilder);
            SetupBatchLog(modelBuilder);
            SetupSocialProfile(modelBuilder);
            SetupRelations(modelBuilder);
            SetupGroup(modelBuilder);
            SetupUserInGroup(modelBuilder);
            //modelBuilder.Entity<TrackRecord>().HasRequired(t => t.Pattern).
        }

        private DbModelBuilder SetupGoal(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>()
                .HasRequired(g => g.Identity)
                .WithMany()
                .HasForeignKey(g => g.IdentityId);

            modelBuilder.Entity<Goal>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            return modelBuilder;
        }

        private DbModelBuilder SetupTask(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
               .HasRequired(g => g.Goal)
               .WithMany()
               .HasForeignKey(g => g.GoalId);

            modelBuilder.Entity<Task>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            return modelBuilder;
        }

        private DbModelBuilder SetupMessage(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Identity).WithMany().HasForeignKey(m => m.IdentityId);
            modelBuilder.Entity<Message>()
                .Property(m => m.Title).HasMaxLength(100).IsRequired();

            return modelBuilder;
        }

        private DbModelBuilder SetupPattern(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pattern>()
                .HasRequired(t => t.Identity)
                .WithMany()
                .HasForeignKey(tr => tr.IdentityId);


            modelBuilder.Entity<Pattern>()
                .Property(p => p.Value).IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_PatternValue") { IsUnique = true }));
            return modelBuilder;
        }

        private static DbModelBuilder SetupTrackRecord(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackRecord>()
                .HasRequired(t => t.Pattern)
                .WithMany()
                .HasForeignKey(tr => tr.PatternId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrackRecord>()
                .HasRequired(t => t.Identity)
                .WithMany()
                .HasForeignKey(tr => tr.LoginId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrackRecord>()
                .Ignore(t => t.PatternValue);

            return modelBuilder;
        }

        private DbModelBuilder SetupIdentity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Identity>().Property(i => i.Login)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_IdentityLogin") { IsUnique = true }));
            modelBuilder.Entity<Identity>().Property(i => i.Id).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Identity>()
                .Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_IdentityEmail") { IsUnique = true }));
            modelBuilder.Entity<Identity>().Property(i => i.Password).IsRequired().HasMaxLength(200);

            //TODO: Set it for Salt

            return modelBuilder;

        }

        private DbModelBuilder SetupPeriod(DbModelBuilder modelBuilder)
        {
            return modelBuilder;
        }

        private DbModelBuilder SetupWasteStatistic(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WasteStatistic>()
                .HasRequired(s => s.Identity)
                .WithMany()
                .HasForeignKey(s => s.IdentityId);

            modelBuilder.Entity<WasteStatistic>()
                            .HasRequired(s => s.Period)
                            .WithMany()
                            .HasForeignKey(s => s.PeriodId);


            return modelBuilder;

        }

        private DbModelBuilder SetupBatchLog(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatchLog>().Property(log => log.Name).IsRequired().HasMaxLength(200);

            return modelBuilder;
        }

        private DbModelBuilder SetupSocialProfile(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialProfile>()
                .HasRequired(sp => sp.Identity);
            modelBuilder.Entity<SocialProfile>()
                .Property(p => p.Nickname)
                .HasMaxLength(30);

            return modelBuilder;
        }

        private DbModelBuilder SetupRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>()
                .HasRequired(r => r.Parent).WithMany().HasForeignKey(r => r.ParentId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Relation>()
                .HasRequired(r => r.Child).WithMany().HasForeignKey(r => r.ChildId);
            modelBuilder.Entity<Relation>()
                .Property(r => r.ParentId).HasColumnAnnotation("Index",
                     new IndexAnnotation(new IndexAttribute("IX_Relation") { IsUnique = true, Order = 1 }));
            modelBuilder.Entity<Relation>()
                .Property(r => r.ChildId).HasColumnAnnotation("Index",
                     new IndexAnnotation(new IndexAttribute("IX_Relation") { IsUnique = true, Order = 2 }));

            return modelBuilder;
        }

        private DbModelBuilder SetupGroup(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().Property(@group => @group.Name).HasMaxLength(30).IsRequired();

            return modelBuilder;
        }

        private DbModelBuilder SetupUserInGroup(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInGroup>()
             .HasRequired(r => r.User).WithMany().HasForeignKey(r => r.UserId)
             .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserInGroup>()
                .HasRequired(r => r.Group).WithMany().HasForeignKey(r => r.GroupId);
            modelBuilder.Entity<UserInGroup>()
                .Property(r => r.UserId).HasColumnAnnotation("Index",
                     new IndexAnnotation(new IndexAttribute("IX_Relation") { IsUnique = true, Order = 1 }));
            modelBuilder.Entity<UserInGroup>()
                .Property(r => r.GroupId).HasColumnAnnotation("Index",
                     new IndexAnnotation(new IndexAttribute("IX_Relation") { IsUnique = true, Order = 2 }));
            modelBuilder.Entity<UserInGroup>()
                .Property(r => r.Type).HasColumnAnnotation("Index",
                     new IndexAnnotation(new IndexAttribute("IX_Relation") { IsUnique = true, Order = 3 }));

            return modelBuilder;
        }
    }
}